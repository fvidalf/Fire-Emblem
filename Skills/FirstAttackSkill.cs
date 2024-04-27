using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SingleCharacterSkills;

namespace Fire_Emblem.Skills;

// First Attack BoostSelfSkill: A bonus skill that only activates on the first attack of the character.
public abstract class FirstAttackSkill : SingleCharacterSkill, ITargetedSkill {
    
    
    protected FirstAttackSkill(string name)
        : base(name) {
    }

    public override void Apply(GameStatus gameStatus) {
        DetermineTarget(gameStatus);
        base.Apply(gameStatus);
        ConcreteApply();
        IsActivated = true;
    }

    protected abstract void ConcreteApply();

    public abstract void DetermineTarget(GameStatus gamestatus);
}