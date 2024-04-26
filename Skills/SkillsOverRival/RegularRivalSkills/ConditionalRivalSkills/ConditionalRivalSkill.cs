using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverRival.RegularRivalSkills.ConditionalRivalSkills;

public abstract class ConditionalRivalSkill: RegularRivalSkill {
    
    protected ConditionalRivalSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name, statsToModify) {}
    
    protected override void ConcreteApply(GameStatus gameStatus) {
        if (IsConditionMet()) {
            foreach (KeyValuePair<EffectType, List<StatEffect>> statEffects in StatsToModify) {
                var effectType = statEffects.Key;
                foreach (var statEffect in statEffects.Value) {
                    UpdateCharacterStat(Character, effectType, statEffect);
                }
            }
        }
    }
    
    protected abstract bool IsConditionMet();
}