using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.GameFiles;

public class SkillHandler {
    
    private readonly CharacterHandler _characterHandler;
    private readonly View _view;
    
    public SkillHandler(View view, CharacterHandler characterHandler) {
        _characterHandler = characterHandler;
        _view = view;
    }
    
    
    public void ApplyCharacterSkills(CharacterModel firstPlayerCharacterModel, CharacterModel secondPlayerCharacterModel) {
        firstPlayerCharacterModel.ResetModifiedStats();
        secondPlayerCharacterModel.ResetModifiedStats();
        var joinedSkills = JoinSkills(new[] {firstPlayerCharacterModel, secondPlayerCharacterModel});
        var orderedSkills = OrderSkills(joinedSkills);
        ApplySkillsJointly(orderedSkills);
    }
    
    private Tuple<CharacterModel, SingleCharacterSkill>[] JoinSkills(CharacterModel[] characters) {
        var skillsPairedToCharacter = new List<Tuple<CharacterModel, SingleCharacterSkill>>();
        foreach (var character in characters) {
            foreach (var skill in character.SingleSkills) {
                skillsPairedToCharacter.Add(new Tuple<CharacterModel, SingleCharacterSkill>(character, skill));
            }
        }
        return skillsPairedToCharacter.ToArray();
    }
    
    private Tuple<CharacterModel, SingleCharacterSkill>[] OrderSkills(Tuple<CharacterModel, SingleCharacterSkill>[] skillsPairedToCharacter) {
        var firstSkillsToApply = new List<Tuple<CharacterModel, SingleCharacterSkill>>();
        var lastSkillsToApply = new List<Tuple<CharacterModel, SingleCharacterSkill>>();
        foreach (var pair in skillsPairedToCharacter) {
            var character = pair.Item1;
            var skill = pair.Item2;
            if (skill is BonusNeutralizer or PenaltyNeutralizer) {
                lastSkillsToApply.Add(new Tuple<CharacterModel, SingleCharacterSkill>(character, skill));
            }
            else {
                firstSkillsToApply.Add(new Tuple<CharacterModel, SingleCharacterSkill>(character, skill));
            }
        }
        firstSkillsToApply.AddRange(lastSkillsToApply);
        return firstSkillsToApply.ToArray();
    }

    private void ApplySkillsJointly(Tuple<CharacterModel, SingleCharacterSkill>[] skillsPairedToCharacter) {
        foreach (var pair in skillsPairedToCharacter) {
            var character = pair.Item1;
            var skill = pair.Item2;
            //if (!skill.IsActivated) character.ApplySkill(skill, character.GameStatus);
            if (!skill.IsActivated) _characterHandler.ApplySkill(character, skill, character.RoundStatus);
        }
    }
    
    public void HandleSkillEffectsNotification(CharacterModel firstPlayerCharacterModel, CharacterModel secondPlayerCharacterModel) {
        var firstPlayerSkillEffects = firstPlayerCharacterModel.GetSkillEffects();
        var secondPlayerSkillEffects = secondPlayerCharacterModel.GetSkillEffects();
        var skillEffects = JoinPlayerSkillEffects(firstPlayerSkillEffects, secondPlayerSkillEffects);
        NotifySkillEffects(skillEffects);
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
    
    private void NotifySkillEffects(Dictionary<CharacterModel,  List<Tuple<EffectType, Stat, int>>> skillEffect) {
        foreach (var character in skillEffect.Keys) {
            foreach (var effect in skillEffect[character]) {
                var effectType = effect.Item1;
                var stat = effect.Item2;
                var amount = effect.Item3;
                switch (effectType) {
                    case EffectType.FirstAttackBonus or EffectType.FirstAttackPenalty:
                        NotifyFirstAttackSkill(character, stat, amount);
                        break;
                    case EffectType.RegularBonus or EffectType.RegularPenalty:
                        NotifyRegularSkill(character, stat, amount);
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
    }
    
    private void NotifyFirstAttackSkill(CharacterModel characterModel, Stat stat, int amount) {
        if (amount != 0) {
            var diffSign = amount > 0 ? "+" : "";
            _view.WriteLine($"{characterModel.Name} obtiene {StatToString.RegularizeMap[stat]}{diffSign}{amount} en su primer ataque");
        } 
    }
    
    private void NotifyRegularSkill(CharacterModel characterModel, Stat stat, int amount) {
        if (amount != 0) {
            var diffSign = amount > 0 ? "+" : "";
            _view.WriteLine($"{characterModel.Name} obtiene {StatToString.Map[stat]}{diffSign}{amount}");
        }
    }
    
    private void NotifyPenaltyNeutralizer(CharacterModel characterModel, Stat stat) {
        _view.WriteLine($"Los penalty de {StatToString.RegularizeMap[stat]} de {characterModel.Name} fueron neutralizados");
    }
    
    private void NotifyBonusNeutralizer(CharacterModel characterModel, Stat stat) {
        _view.WriteLine($"Los bonus de {StatToString.RegularizeMap[stat]} de {characterModel.Name} fueron neutralizados");
    }
}