using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.FirstAttackSkills;

public abstract class FirstAttackSkill : SingleCharacterSkill, ITargetedSkill {
    
    
    protected FirstAttackSkill(string name)
        : base(name) {
    }

    public override void Apply(RoundStatus roundStatus) {
        DetermineTarget(roundStatus);
        base.Apply(roundStatus);
        ConcreteApply();
        IsActivated = true;
    }

    protected abstract void ConcreteApply();

    public abstract void DetermineTarget(RoundStatus gamestatus);
}