using System.Reflection;
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
    
    protected void UpdateCharacterStat(Character character, KeyValuePair<Stat, int> stat) {
        var characterStat = GetCharacterStat(character, stat.Key);
        var characterStatValue = GetCharacterStatValue(characterStat, stat.Key);
        
        int newStatValue = (int) characterStatValue + stat.Value;
        characterStat.SetValue(character, newStatValue);
        
        UpdateModifiedStats(stat);
    }
    
    protected PropertyInfo GetCharacterStat(Character character, Stat stat) {
        var statAsString = StatToString.Map[stat];
        var characterStat = character.GetType().GetProperty(statAsString);
        if (characterStat is null) throw new InvalidOperationException();
        return characterStat;
    }
    
    protected int GetCharacterStatValue(PropertyInfo characterStat, Stat stat) {
        var characterStatValue = characterStat.GetValue(Character);
        if (characterStatValue is null) throw new InvalidOperationException();
        return (int) characterStatValue;
    }
    
    protected void UpdateModifiedStats(KeyValuePair<Stat, int> stat) {
        ModifiedStats[stat.Key] = stat.Value;
    }
    
    protected void SetModifiedStats() {
        SkillEffect.Stats = ModifiedStats;
    }
    
    public virtual void Reset() {
        SkillEffect = new SkillEffect();
        ModifiedStats = new Dictionary<Stat, int>();
        IsActivated = false;
        SetEffectType();
    }
    
    protected abstract void SetEffectType();
}