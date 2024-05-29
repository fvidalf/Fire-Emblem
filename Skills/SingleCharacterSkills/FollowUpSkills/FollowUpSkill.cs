using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.FollowUpSkills;

public abstract class FollowUpSkill : SingleCharacterSkill, ITargetedSkill {
    
    
    protected FollowUpSkill(string name)
        : base(name) {
    }

    public override void Apply(RoundStatus roundStatus) {
        DetermineTarget(roundStatus);
        base.Apply(roundStatus);
        ConcreteApply(roundStatus);
        IsActivated = true;
    }

    protected abstract void ConcreteApply(RoundStatus roundStatus);

    public abstract void DetermineTarget(RoundStatus gamestatus);
}