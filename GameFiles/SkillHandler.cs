using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers;
using Fire_Emblem.Skills.SingleSkills.Neutralizers.PenaltyNeutralizers;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.GameFiles;

public class SkillHandler(View view, CharacterHandler characterHandler) {

    public void ApplyCharacterSkills(CharacterModel firstPlayerCharacter, CharacterModel secondPlayerCharacter) {
        firstPlayerCharacter.ResetModifiedStats();
        secondPlayerCharacter.ResetModifiedStats();
        var joinedSkills = JoinSkills(new[] {firstPlayerCharacter, secondPlayerCharacter});
        var orderedSkills = OrderSkills(joinedSkills);
        ApplySkillsJointly(orderedSkills);
    }
    
    private static Tuple<CharacterModel, ISingleSkill>[] JoinSkills(CharacterModel[] characters) {
        var skillsPairedToCharacter = new List<Tuple<CharacterModel, ISingleSkill>>();
        foreach (var character in characters) {
            foreach (var skill in character.SingleSkills) {
                skillsPairedToCharacter.Add(new Tuple<CharacterModel, ISingleSkill>(character, skill));
            }
        }
        return skillsPairedToCharacter.ToArray();
    }
    
    private static Tuple<CharacterModel, ISingleSkill>[] OrderSkills(Tuple<CharacterModel, ISingleSkill>[] skillsPairedToCharacter) {
        var firstSkillsToApply = new List<Tuple<CharacterModel, ISingleSkill>>();
        var secondSkillsToApply = new List<Tuple<CharacterModel, ISingleSkill>>();
        var thirdSkillsToApply = new List<Tuple<CharacterModel, ISingleSkill>>();
        var fourthSkillsToApply = new List<Tuple<CharacterModel, ISingleSkill>>();
        foreach (var (character, skill) in skillsPairedToCharacter) {
            switch (skill) {
                case BonusNeutralizer or PenaltyNeutralizer:
                    secondSkillsToApply.Add(new Tuple<CharacterModel, ISingleSkill>(character, skill));
                    break;
                case DivineRecreationNextAttackDamageIncreaseSkill:
                    fourthSkillsToApply.Add(new Tuple<CharacterModel, ISingleSkill>(character, skill));
                    break;
                case DamageModifierSkill:
                    thirdSkillsToApply.Add(new Tuple<CharacterModel, ISingleSkill>(character, skill));
                    break;
                default:
                    firstSkillsToApply.Add(new Tuple<CharacterModel, ISingleSkill>(character, skill));
                    break;
            }
        }
        firstSkillsToApply.AddRange(secondSkillsToApply);
        firstSkillsToApply.AddRange(thirdSkillsToApply);
        fourthSkillsToApply.Reverse();
        firstSkillsToApply.AddRange(fourthSkillsToApply);
        return firstSkillsToApply.ToArray();
    }

    private void ApplySkillsJointly(Tuple<CharacterModel, ISingleSkill>[] skillsPairedToCharacter) {
        foreach (var pair in skillsPairedToCharacter) {
            var character = pair.Item1;
            var skill = pair.Item2;
            characterHandler.ApplySkill(character, skill, character.RoundStatus);
        }
    }
    
    public void HandleSkillEffectsNotification(CharacterModel firstPlayerCharacter, CharacterModel secondPlayerCharacter) {
        var firstPlayerSkillEffects = firstPlayerCharacter.GetSkillEffects();
        var secondPlayerSkillEffects = secondPlayerCharacter.GetSkillEffects();
        var skillEffects = JoinPlayerSkillEffects(firstPlayerSkillEffects, secondPlayerSkillEffects);
        
        NotifyCharacterSkillEffects(firstPlayerCharacter, skillEffects);
        NotifyDamageModifiers(firstPlayerCharacter);
        NotifyCharacterSkillEffects(secondPlayerCharacter, skillEffects);
        NotifyDamageModifiers(secondPlayerCharacter);
    }
    
    private Dictionary<CharacterModel, List<Tuple<EffectType, Stat, int>>> JoinPlayerSkillEffects(Dictionary<CharacterModel, SkillEffect> firstPlayerSkillEffects, Dictionary<CharacterModel, SkillEffect> secondPlayerSkillEffects) {
        var joinedSkillEffects = new Dictionary<CharacterModel, List<Tuple<EffectType, Stat, int>>>();
        foreach (var character in firstPlayerSkillEffects.Keys) {
            var firstPlayerEffects = firstPlayerSkillEffects[character];
            var secondPlayerEffects = secondPlayerSkillEffects[character];

            firstPlayerEffects.Join(secondPlayerEffects); 
            var sortedEffects = GetSortedEffects(firstPlayerEffects);
            joinedSkillEffects[character] = sortedEffects;
        }
        return joinedSkillEffects;
    }
    
    private List<Tuple<EffectType, Stat, int>> GetSortedEffects(SkillEffect effects) {
        var simpleEffects = effects.CollapseIntoList();
        var effectsSortedByEffectType = simpleEffects.OrderBy(effect => effect.Item1);
        var effectsSortedByStat = effectsSortedByEffectType.ThenBy(effect => effect.Item2);
       
        return effectsSortedByStat.ToList();
    }
    
    private void NotifyCharacterSkillEffects(CharacterModel character, Dictionary<CharacterModel,  List<Tuple<EffectType, Stat, int>>> skillEffects) {
        var characterSkillEffects = skillEffects[character];
        foreach (var (effectType, stat, amount) in characterSkillEffects) {
            switch (effectType) {
                case EffectType.FirstAttackBonus or EffectType.FirstAttackPenalty:
                    NotifyFirstAttackSkill(character, stat, amount);
                    break;
                case EffectType.RegularBonus or EffectType.RegularPenalty:
                    NotifyRegularSkill(character, stat, amount);
                    break;
                case EffectType.FollowUpBonus or EffectType.FollowUpPenalty:
                    NotifyFollowUpAttackSkill(character, stat, amount);
                    break;
                case EffectType.PenaltyNeutralizer:
                    NotifyPenaltyNeutralizer(character, stat);
                    break;
                case EffectType.BonusNeutralizer:
                    NotifyBonusNeutralizer(character, stat);
                    break;
            }
        }
    }
    
    private void NotifyFirstAttackSkill(CharacterModel characterModel, Stat stat, int amount) {
        if (amount != 0) {
            var diffSign = amount > 0 ? "+" : "";
            view.WriteLine($"{characterModel.Name} obtiene {StatSimplifier.RegularizeMap[stat]}{diffSign}{amount} en su primer ataque");
        } 
    }
    
    private void NotifyRegularSkill(CharacterModel characterModel, Stat stat, int amount) {
        if (amount != 0) {
            var diffSign = amount > 0 ? "+" : "";
            view.WriteLine($"{characterModel.Name} obtiene {StatSimplifier.Map[stat]}{diffSign}{amount}");
        }
    }
    
    private void NotifyFollowUpAttackSkill(CharacterModel characterModel, Stat stat, int amount) {
        if (amount != 0) {
            var diffSign = amount > 0 ? "+" : "";
            view.WriteLine($"{characterModel.Name} obtiene {StatSimplifier.RegularizeMap[stat]}{diffSign}{amount} en su Follow-Up");
        }
    }
    
    private void NotifyPenaltyNeutralizer(CharacterModel characterModel, Stat stat) {
        view.WriteLine($"Los penalty de {StatSimplifier.RegularizeMap[stat]} de {characterModel.Name} fueron neutralizados");
    }
    
    private void NotifyBonusNeutralizer(CharacterModel characterModel, Stat stat) {
        view.WriteLine($"Los bonus de {StatSimplifier.RegularizeMap[stat]} de {characterModel.Name} fueron neutralizados");
    }
    
    private void NotifyDamageModifiers(CharacterModel character) {
        var damageModifiers = character.GetDamageModifiers();
        foreach (var effectType in damageModifiers.DamageModifiersByEffectType.Keys) {
            var amount = damageModifiers.DamageModifiersByEffectType[effectType];
            switch (effectType) {
                case EffectType.RegularDamageIncrease:
                    NotifyRegularDamageIncrease(character, (int) amount);
                    break;
                case EffectType.FirstAttackDamageIncrease:
                    NotifyFirstAttackDamageIncrease(character, (int) amount);
                    break;
                case EffectType.FollowUpDamageIncrease:
                    NotifyFollowUpDamageIncrease(character, (int) amount);
                    break;
                case EffectType.RegularDamagePercentageReduction:
                    NotifyRegularPercentageDamageReduction(character, amount);
                    break;
                case EffectType.FirstAttackDamagePercentageReduction:
                    NotifyFirstAttackPercentageDamageReduction(character, amount);
                    break;
                case EffectType.FollowUpDamagePercentageReduction:
                    NotifyFollowUpDamagePercentageReduction(character, amount);
                    break;
                case EffectType.RegularDamageAbsoluteReduction:
                    NotifyRegularAbsoluteDamageReduction(character, (int) amount);
                    break;
            }
        }
    }
    
    private void NotifyRegularDamageIncrease(CharacterModel character, int amount) {
        if (amount != 0) {
            view.WriteLine($"{character.Name} realizará +{amount} daño extra en cada ataque");
        }
    }
    
    private void NotifyFirstAttackDamageIncrease(CharacterModel character, int amount) {
        if (amount != 0) {
            view.WriteLine($"{character.Name} realizará +{amount} daño extra en su primer ataque");
        }
    }
    
    private void NotifyFollowUpDamageIncrease(CharacterModel character, int amount) {
        if (amount != 0) {
            view.WriteLine($"{character.Name} realizará +{amount} daño extra en su Follow-Up");
        }
    }
    
    private void NotifyRegularPercentageDamageReduction(CharacterModel character, double amount) {
        if (amount != 0) {
            var percentage = Math.Round(amount * 100, 0);
            view.WriteLine($"{character.Name} reducirá el daño de los ataques del rival en un {percentage}%");
        }
    }
    
    private void NotifyFirstAttackPercentageDamageReduction(CharacterModel character, double amount) {
        if (amount != 0) {
            var percentage = Math.Round(amount * 100, 0);
            view.WriteLine($"{character.Name} reducirá el daño del primer ataque del rival en un {percentage}%");
        }
    }
    
    private void NotifyFollowUpDamagePercentageReduction(CharacterModel character, double amount) {
        if (amount != 0) {
            var percentage = Math.Round(amount * 100, 0);
            view.WriteLine($"{character.Name} reducirá el daño del Follow-Up del rival en un {percentage}%");
        }
    }
    
    private void NotifyRegularAbsoluteDamageReduction(CharacterModel character, int amount) {
        if (amount != 0) {
            view.WriteLine($"{character.Name} recibirá -{amount} daño en cada ataque");
        }
    }
}