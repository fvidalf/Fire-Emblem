using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills;
using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.CombatHpModificationSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers;
using Fire_Emblem.Skills.SingleSkills.Neutralizers.PenaltyNeutralizers;
using Fire_Emblem.Skills.SkillEffectFiles;
using Fire_Emblem.Skills.SkillEffectFiles.SortedEffectsFiles;

namespace Fire_Emblem.GameFiles;

public class SkillHandler(View view, CharacterHandler characterHandler) {
    
    public void ApplyPreCombatSkills(CharacterModel firstPlayerCharacter, CharacterModel secondPlayerCharacter) {
        firstPlayerCharacter.ResetModifiedStats();
        secondPlayerCharacter.ResetModifiedStats();
        var joinedSkills = JoinSkills(new[] {firstPlayerCharacter, secondPlayerCharacter}, TimeToApply.PreCombat);
        var orderedSkills = OrderSkills(joinedSkills);
        ApplySkillsJointly(orderedSkills);
    }
    
    public void ApplyCombatSkills(CharacterModel firstPlayerCharacter, CharacterModel secondPlayerCharacter) {
        var joinedSkills = JoinSkills(new[] {firstPlayerCharacter, secondPlayerCharacter}, TimeToApply.Combat);
        var orderedSkills = OrderSkills(joinedSkills);
        ApplySkillsJointly(orderedSkills);
    }
    
    public void ApplyPostCombatSkills(CharacterModel firstPlayerCharacter, CharacterModel secondPlayerCharacter) {
        var joinedSkills = JoinSkills(new[] {firstPlayerCharacter, secondPlayerCharacter}, TimeToApply.PostCombat);
        var orderedSkills = OrderSkills(joinedSkills);
        ApplySkillsJointly(orderedSkills);
    }
    
    private static Tuple<CharacterModel, ISingleSkill>[] JoinSkills(CharacterModel[] characters, TimeToApply timeToApply) {
        var skillsPairedToCharacter = new List<Tuple<CharacterModel, ISingleSkill>>();
        foreach (var character in characters) {
            foreach (var skill in character.SingleBaseSkills) {
                if (skill is ITimedSkill timedSkill && timedSkill.TimeToApply == timeToApply) {
                    skillsPairedToCharacter.Add(new Tuple<CharacterModel, ISingleSkill>(character, skill));
                } 
            }
        }
        return skillsPairedToCharacter.ToArray();
    }
    
    private static Tuple<CharacterModel, ISingleSkill>[] OrderSkills(Tuple<CharacterModel, ISingleSkill>[] skillsPairedToCharacter) {
        var firstSkillsToApply = new List<Tuple<CharacterModel, ISingleSkill>>();
        var secondSkillsToApply = new List<Tuple<CharacterModel, ISingleSkill>>();
        var thirdSkillsToApply = new List<Tuple<CharacterModel, ISingleSkill>>();
        var fourthSkillsToApply = new List<Tuple<CharacterModel, ISingleSkill>>();
        var fifthSkillsToApply = new List<Tuple<CharacterModel, ISingleSkill>>();
        foreach (var (character, skill) in skillsPairedToCharacter) {
            switch (skill) {
                case BonusNeutralizer or PenaltyNeutralizer:
                    secondSkillsToApply.Add(new Tuple<CharacterModel, ISingleSkill>(character, skill));
                    break;
                case DivineRecreationNextAttackDamageIncreaseSkill:
                    fifthSkillsToApply.Add(new Tuple<CharacterModel, ISingleSkill>(character, skill));
                    break;
                case PreCombatHpModificationSkill:
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
        firstSkillsToApply.AddRange(fifthSkillsToApply);
        return firstSkillsToApply.ToArray();
    }

    private void ApplySkillsJointly(Tuple<CharacterModel, ISingleSkill>[] skillsPairedToCharacter) {
        foreach (var pair in skillsPairedToCharacter) {
            var character = pair.Item1;
            var skill = pair.Item2;
            characterHandler.ApplySkill(character, skill, character.RoundStatus);
        }
    }
    
    public void HandlePreCombatSkillEffectsNotification(CharacterModel firstPlayerCharacter, CharacterModel secondPlayerCharacter) {
        var firstPlayerSkillEffects = firstPlayerCharacter.GetSkillEffects();
        var secondPlayerSkillEffects = secondPlayerCharacter.GetSkillEffects();
        var skillEffects = JoinPlayerSkillEffects(firstPlayerSkillEffects, secondPlayerSkillEffects);
        
        NotifyCharacterSkillEffects(firstPlayerCharacter, skillEffects[firstPlayerCharacter]);
        NotifyDamageModifiers(firstPlayerCharacter);
        NotifyHealing(firstPlayerCharacter, skillEffects[firstPlayerCharacter]);
        NotifyCharacterSkillEffects(secondPlayerCharacter, skillEffects[secondPlayerCharacter]);
        NotifyDamageModifiers(secondPlayerCharacter);
        NotifyHealing(secondPlayerCharacter, skillEffects[secondPlayerCharacter]);
    }
    
    public void HandleCombatSkillEffectsNotification(CharacterModel firstPlayerCharacter, CharacterModel secondPlayerCharacter) {
        var firstPlayerSkillEffects = firstPlayerCharacter.GetSkillEffects();
        var secondPlayerSkillEffects = secondPlayerCharacter.GetSkillEffects();
        var skillEffects = JoinPlayerSkillEffects(firstPlayerSkillEffects, secondPlayerSkillEffects);
        
        NotifyInCombatHealing(firstPlayerCharacter, skillEffects[firstPlayerCharacter]);
        NotifyInCombatHealing(secondPlayerCharacter, skillEffects[secondPlayerCharacter]);
    }
    
    public void HandlePostCombatSkillEffectsNotification(CharacterModel firstPlayerCharacter, CharacterModel secondPlayerCharacter) {
        var firstPlayerSkillEffects = firstPlayerCharacter.GetSkillEffects();
        var secondPlayerSkillEffects = secondPlayerCharacter.GetSkillEffects();
        var skillEffects = JoinPlayerSkillEffects(firstPlayerSkillEffects, secondPlayerSkillEffects);
        
        NotifyPostCombatHpModification(firstPlayerCharacter, skillEffects[firstPlayerCharacter]);
        NotifyPostCombatHpModification(secondPlayerCharacter, skillEffects[secondPlayerCharacter]);
    }
    
    private Dictionary<CharacterModel, SortedEffects> JoinPlayerSkillEffects(Dictionary<CharacterModel, SkillEffect> firstPlayerSkillEffects, Dictionary<CharacterModel, SkillEffect> secondPlayerSkillEffects) {
        var joinedSkillEffects = new Dictionary<CharacterModel, SortedEffects>();
        foreach (var character in firstPlayerSkillEffects.Keys) {
            var firstPlayerEffects = firstPlayerSkillEffects[character];
            var secondPlayerEffects = secondPlayerSkillEffects[character];

            firstPlayerEffects.Join(secondPlayerEffects); 
            var sortedEffects = firstPlayerEffects.GetSortedEffects();
            joinedSkillEffects[character] = sortedEffects;
        }
        return joinedSkillEffects;
    }
    
    private void NotifyCharacterSkillEffects(CharacterModel character, SortedEffects skillEffects) {
        foreach (var (effectType, stat, amount) in skillEffects) {
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
    
    private void NotifyHealing(CharacterModel character, SortedEffects skillEffects) {
        foreach (var (effectType, stat, amount) in skillEffects) {
            if (effectType == EffectType.HpBonusByPercentage) {
                if (amount != 0) {
                    view.WriteLine($"{character.Name} recuperará HP igual al {amount}% del daño realizado en cada ataque");
                }
            }
            if (effectType == EffectType.HpModificationPreCombat) {
                if (amount < 0) {
                    view.WriteLine($"{character.Name} recibe {-amount} de daño antes de iniciar el combate y queda con {character.Hp} HP");
                
                }
            }
        }
    }
    
    private void NotifyInCombatHealing(CharacterModel character, SortedEffects skillEffects) {
        foreach (var (effectType, stat, amount) in skillEffects) {
            if (effectType == EffectType.HpModificationInCombat) {
                if (amount != 0) {
                    view.WriteLine($"{character.Name} recupera {amount} HP luego de atacar y queda con {character.Hp} HP.");
                }
            }
        }
        character.ResetHealingSkillEffect();
    }
    
    private void NotifyPostCombatHpModification(CharacterModel character, SortedEffects skillEffects) {
        foreach (var (effectType, stat, amount) in skillEffects) {
            if (effectType == EffectType.HpModificationPostCombat) {
                if (character.IsDead) {
                    return;
                }
                if (amount > 0) {
                    view.WriteLine($"{character.Name} recupera {amount} HP despues del combate");
                } else if (amount < 0) {
                    view.WriteLine($"{character.Name} recibe {-amount} de daño despues del combate");
                }
            }
        }
        character.ResetHealingSkillEffect();
    }
}