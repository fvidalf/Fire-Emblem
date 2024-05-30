using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.FirstAttackSkills;

public abstract class FirstAttackSkill : StatModifierSkill, ITargetedSkill {
    
    
    protected FirstAttackSkill(string name)
        : base(name) {
    }

    public override void Apply(RoundStatus roundStatus) {
        base.Apply(roundStatus);
        DetermineTarget();
        ConcreteApply();
    }

    protected abstract void ConcreteApply();

    public abstract void DetermineTarget();
}