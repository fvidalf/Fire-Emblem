using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills;

// Regular Bonus: A bonus skill that is applied at the start of the turn and lasts indefinitely.
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