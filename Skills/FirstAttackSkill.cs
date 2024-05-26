using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SingleCharacterSkills;

namespace Fire_Emblem.Skills;

// First Attack BoostSelfSkill: A bonus skill that only activates on the first attack of the character.
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