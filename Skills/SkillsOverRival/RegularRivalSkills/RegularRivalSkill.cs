using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverRival.RegularRivalSkills;

// BoostSkill Over Rival: A skill that ONLY affects the rival character to the one who applies the skill.
public abstract class RegularRivalSkill: RegularSkill, ITargetedSkill {
    
    protected RegularRivalSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify) 
        :base (name, statsToModify) {}

    public override void Apply(GameStatus gameStatus) {
        DetermineTarget(gameStatus.RivalCharacter);
        base.Apply(gameStatus);
    }
    
    public void DetermineTarget(Character character) {
        Character = character;
    }
}