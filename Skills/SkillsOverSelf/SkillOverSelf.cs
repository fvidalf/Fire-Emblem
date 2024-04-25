using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf;

// Skill Over Self: A skill that ONLY affects the character that has the skill.
public abstract class SkillOverSelf: IBaseSkill{
    public string Name { get; set; }
    public bool IsActivated { get; set; }
    protected Character? Character;
    public SkillEffect SkillEffect { get; set; }
    protected GameStatus GameStatus;
    protected Dictionary<Stat, int> ModifiedStats;
    
    protected SkillOverSelf(string name) {
        Name = name;
        IsActivated = false; 
        SkillEffect = new SkillEffect();
        ModifiedStats = new Dictionary<Stat, int>();
    }

    public virtual void Apply(GameStatus gameStatus) {
        Character = gameStatus.ActivatingCharacter;
        GameStatus = gameStatus;
    }
    
    public Dictionary<Character, SkillEffect> GetModifiedStats() {
        return new Dictionary<Character, SkillEffect> { {Character, SkillEffect} };
    }
    
    public void Reset() {
        SkillEffect = new SkillEffect();
        ModifiedStats = new Dictionary<Stat, int>();
    }
    
    protected abstract void SetEffectType();
}