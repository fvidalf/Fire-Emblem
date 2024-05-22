using System.Reflection;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
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
    
    public virtual void Reset() {
        SkillEffect = new SkillEffect();
        IsActivated = false;
    }
    
    protected virtual void UpdateStat(Character character, EffectType effectType, StatEffect statEffect) {
        var characterStat = GetCharacterStat(character, statEffect.Stat);
        
        UpdateCharacterStat(characterStat, character, statEffect);
        UpdateCharacterModifiers(character, effectType, statEffect);
        UpdateModifiedStats(effectType, statEffect);
    }
    
    protected PropertyInfo GetCharacterStat(Character character, Stat stat) {
        var statAsString = StatToString.Map[stat];
        var characterStat = character.GetType().GetProperty(statAsString);
        return characterStat;
    }

    private void UpdateCharacterStat(PropertyInfo characterStat, Character character, StatEffect statEffect) {
        var characterStatValue = GetCharacterStatValue(characterStat, statEffect.Stat);

        int newStatValue = (int)characterStatValue + statEffect.Amount;
        characterStat.SetValue(character, newStatValue);
    }
    
    protected int GetCharacterStatValue(PropertyInfo characterStat, Stat stat) {
        var characterStatValue = characterStat.GetValue(Character);
        return (int) characterStatValue;
    }
    
    protected void UpdateCharacterModifiers(Character character, EffectType effectType, StatEffect statEffect) {
        character.StatModifiers.UpdateModifiers(effectType, statEffect);
    }
    
    protected void UpdateModifiedStats(EffectType effectType, StatEffect statEffect) {
        if (SkillEffect.StatEffectsByEffectType.ContainsKey(effectType)) {
            var statEffects = SkillEffect.StatEffectsByEffectType[effectType];
            statEffects.Add(statEffect);
        }
        else {
            var statEffects = new List<StatEffect> {statEffect};
            SkillEffect.StatEffectsByEffectType[effectType] = statEffects;
        }
    }
    
    
    
}