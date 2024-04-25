using System.Reflection;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf;

// BoostSkill Over Self: A skill that ONLY affects the character that has the skill.
public abstract class SkillOverSelf: BaseSkill{
    
    protected SkillOverSelf(string name) 
        :base (name) {}

    public override void Apply(GameStatus gameStatus) {
        Character = gameStatus.ActivatingCharacter;
        base.Apply(gameStatus);
    }
}