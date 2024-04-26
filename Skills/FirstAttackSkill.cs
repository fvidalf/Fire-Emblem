using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills;

// First Attack BoostSelfSkill: A bonus skill that only activates on the first attack of the character.
public abstract class FirstAttackSkill : BaseSkill, ITargetedSkill {
    
    
    protected FirstAttackSkill(string name)
        : base(name) {
    }

    public override void Apply(GameStatus gameStatus) {
        // I can check here whether this is NOT this unit's attack phase.
        // If so, I can return early and not count this as an application of the skill.
        DetermineTarget(gameStatus.ActivatingCharacter);
        base.Apply(gameStatus);
        ConcreteApply();
        IsActivated = true;
    }

    protected abstract void ConcreteApply();

    public void DetermineTarget(Character character) {
        Character = character;
    }
}