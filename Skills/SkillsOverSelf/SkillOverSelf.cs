using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf;

public abstract class SkillOverSelf: IBaseSkill{
    public string Name { get; set; }
    public bool IsActivated { get; set; }
    protected Character? Character;
    public SkillEffect SkillEffect { get; set; }
    
    protected SkillOverSelf(string name) {
        Name = name;
        IsActivated = false; 
        SkillEffect = new SkillEffect();
    }

    public void Apply(GameStatus gameStatus) {
        Character = gameStatus.ActivatingCharacter;
        ConcreteApply(Character);
        IsActivated = true;
    }

    protected abstract void ConcreteApply(Character character);

    public Dictionary<Character, SkillEffect> GetModifiedStats() {
        return new Dictionary<Character, SkillEffect> { {Character, SkillEffect} };
    }
    
    protected abstract void SetEffectType();
}