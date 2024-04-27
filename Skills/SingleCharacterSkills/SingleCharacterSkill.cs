using System.Reflection;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills;

public abstract class SingleCharacterSkill: ISingleCharacterSkill {
    
    public string Name { get; set; }
    public bool IsActivated { get; set; }
    public Character? Character;
    public SkillEffect SkillEffect { get; set; }
    protected GameStatus GameStatus;
    
    protected SingleCharacterSkill(string name) {
        Name = name;
        IsActivated = false; 
        SkillEffect = new SkillEffect();
    }
    
    public virtual void Apply(GameStatus gameStatus) {
        GameStatus = gameStatus;
    }
    
    public Dictionary<Character, SkillEffect> GetModifiedStats() {
        return new Dictionary<Character, SkillEffect> { {Character, SkillEffect} };
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
    
    public virtual void Reset() {
        SkillEffect = new SkillEffect();
        IsActivated = false;
    }
    
    protected virtual void UpdateCharacterStat(Character character, EffectType effectType, StatEffect statEffect) {
        var characterStat = GetCharacterStat(character, statEffect.Stat);
        var characterStatValue = GetCharacterStatValue(characterStat, statEffect.Stat);

        int newStatValue = (int)characterStatValue + statEffect.Amount;
        characterStat.SetValue(character, newStatValue);
        UpdateCharacterMemory(character, effectType, statEffect);
        
        UpdateModifiedStats(effectType, statEffect);
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
    
    protected void UpdateCharacterMemory(Character character, EffectType effectType, StatEffect statEffect) {
        // If it is a bonus, update the corresponding memory var in Character
        Console.WriteLine("Skill siendo aplicada: " + Name);
        switch (statEffect.Stat) {
            case (Stat.Hp):
                character.HpModifiers[effectType] += statEffect.Amount;
                break;
            case (Stat.Def):
                character.DefModifiers[effectType] += statEffect.Amount;
                break;
            case (Stat.Res):
                character.ResModifiers[effectType] += statEffect.Amount;
                break;
            case (Stat.Atk):
                character.AtkModifiers[effectType] += statEffect.Amount;
                break;
            case (Stat.Spd):
                character.SpdModifiers[effectType] += statEffect.Amount;
                break;
        }
    }
    
}