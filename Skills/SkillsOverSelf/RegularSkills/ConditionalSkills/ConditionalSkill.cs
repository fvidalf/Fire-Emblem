using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.ConditionalSkills;

// Conditional Bonus: A regular bonus skill with a condition that must be met before the bonus is applied.
public abstract class ConditionalSkill: RegularSkill {
    
    protected ConditionalSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
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