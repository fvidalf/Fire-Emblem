using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills;

public abstract class RegularSkill: StatModifierSkill {
    
    protected Dictionary<EffectType, List<StatEffect>> StatsToModify;
    
    protected RegularSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name) {
        StatsToModify = statsToModify;
    }
    
    public override void Apply(RoundStatus roundStatus) {
        base.Apply(roundStatus);
        ConcreteApply(roundStatus);
    }
    
    protected abstract void ConcreteApply(RoundStatus roundStatus);
}