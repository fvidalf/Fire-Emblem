using System.Reflection;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf;

// BoostSkill Over Self: A skill that ONLY affects the character that has the skill.
public abstract class SkillOverSelf: IBaseSkill{
    public string Name { get; set; }
    public bool IsActivated { get; set; }
    protected Character? Character;
    public SkillEffect SkillEffect { get; set; }
    protected GameStatus GameStatus;
    
    protected SkillOverSelf(string name) {
        Name = name;
        IsActivated = false; 
        SkillEffect = new SkillEffect();
    }

    public virtual void Apply(GameStatus gameStatus) {
        Character = gameStatus.ActivatingCharacter;
        GameStatus = gameStatus;
    }
    
    public Dictionary<Character, SkillEffect> GetModifiedStats() {
        SkillEffect.ConsoleWriteStats();
        return new Dictionary<Character, SkillEffect> { {Character, SkillEffect} };
    }
    
    protected virtual void UpdateCharacterStat(Character character, EffectType effectType, StatEffect statEffect) {
        var characterStat = GetCharacterStat(character, statEffect.Stat);
        var characterStatValue = GetCharacterStatValue(characterStat, statEffect.Stat);

        int newStatValue = (int)characterStatValue + statEffect.Amount;
        characterStat.SetValue(character, newStatValue);
        
        UpdateModifiedStats(effectType, statEffect);
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
    
    protected void UpdateModifiedStats(EffectType effectType, StatEffect statEffect) {
        // Check if the effect type is already in the dictionary
        if (SkillEffect.StatEffectsByEffectType.ContainsKey(effectType)) {
            var statEffects = SkillEffect.StatEffectsByEffectType[effectType];
            statEffects.Add(statEffect);
        }
        else {
            // Create a new list with the statEffect and add it to the dictionary
            var statEffects = new List<StatEffect> {statEffect};
            SkillEffect.StatEffectsByEffectType[effectType] = statEffects;
        }
    }
    
    public virtual void Reset() {
        SkillEffect = new SkillEffect();
        IsActivated = false;
    }
}