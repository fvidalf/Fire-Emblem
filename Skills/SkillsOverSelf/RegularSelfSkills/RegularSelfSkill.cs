using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills;

// BoostSkill Over Self: A skill that ONLY affects the character that has the skill.
public abstract class RegularSelfSkill: RegularSkill, ITargetedSkill {
    
    protected RegularSelfSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify) 
        :base (name, statsToModify) {}

    public override void Apply(GameStatus gameStatus) {
        DetermineTarget(gameStatus.ActivatingCharacter);
        base.Apply(gameStatus);
    }
    
    public void DetermineTarget(Character character) {
        Character = character;
    }
    
    
}