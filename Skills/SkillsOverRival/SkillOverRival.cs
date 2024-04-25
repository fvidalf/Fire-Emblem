using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverRival;

// BoostSkill Over Rival: A skill that ONLY affects the rival character to the one who applies the skill.
public abstract class SkillOverRival: BaseSkill{
    
    protected SkillOverRival(string name) 
        :base (name) {}

    public override void Apply(GameStatus gameStatus) {
        Character = gameStatus.RivalCharacter;
        base.Apply(gameStatus);
    }
}