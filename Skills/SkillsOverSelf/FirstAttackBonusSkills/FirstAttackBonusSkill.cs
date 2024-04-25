using System.Reflection;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.FirstAttackBonusSkills;

// First Attack BoostSkill: A bonus skill that only activates on the first attack of the character.
public abstract class FirstAttackBonusSkill : SkillOverSelf {
    
    protected FirstAttackBonusSkill(string name)
        : base(name) {
    }

    public override void Apply(GameStatus gameStatus) {
        // I can check here whether this is NOT this unit's attack phase.
        // If so, I can return early and not count this as an application of the skill.
        base.Apply(gameStatus);
        ConcreteApply();
        IsActivated = true;
    }

    protected abstract void ConcreteApply();
}