using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills.SingleSkills.FollowUpSkills;

public abstract class FollowUpSkill : StatModifierSkill, ITargetedSkill {
    
    
    protected FollowUpSkill(string name)
        : base(name) {
    }

    public override void Apply(RoundStatus roundStatus) {
        base.Apply(roundStatus);
        DetermineTarget();
        ConcreteApply(roundStatus);
    }

    protected abstract void ConcreteApply(RoundStatus roundStatus);

    public abstract void DetermineTarget();
}