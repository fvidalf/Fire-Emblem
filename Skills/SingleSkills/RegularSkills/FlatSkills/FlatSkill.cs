using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills;

public abstract class FlatSkill: RegularSkill, ITargetedSkill {
    
    protected FlatSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
    : base(name, statsToModify) {}
    
    protected override void ConcreteApply(RoundStatus roundStatus) {
        DetermineTarget();
        foreach (KeyValuePair<EffectType, List<StatEffect>> statEffects in StatsToModify) {
            var effectType = statEffects.Key;
            foreach (var statEffect in statEffects.Value) {
                UpdateStat(Character, effectType, statEffect);
            }
        }
    }

    public abstract void DetermineTarget();
}