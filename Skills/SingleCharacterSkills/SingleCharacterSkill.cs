using System.Reflection;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills;

public abstract class SingleCharacterSkill: ISingleCharacterSkill {
    
    public string Name { get; set; }
    public bool IsActivated { get; set; }
    public CharacterModel? Character;
    public SkillEffect SkillEffect { get; set; }
    protected RoundStatus RoundStatus;
    
    protected SingleCharacterSkill(string name) {
        Name = name;
        IsActivated = false; 
        SkillEffect = new SkillEffect();
    }
    
    public virtual void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
    }
    
    public Dictionary<CharacterModel, SkillEffect> GetModifiedStats() {
        return new Dictionary<CharacterModel, SkillEffect> { {Character, SkillEffect} };
    }
    
    public virtual void Reset() {
        SkillEffect = new SkillEffect();
        IsActivated = false;
    }
    
    protected virtual void UpdateStat(CharacterModel characterModel, EffectType effectType, StatEffect statEffect) {
        var characterStat = GetCharacterStat(characterModel, statEffect.Stat);
        var newStatEffect = new StatEffect(statEffect.Stat, statEffect.Amount);
        UpdateCharacterStat(characterStat, characterModel, newStatEffect);
        UpdateCharacterModifiers(characterModel, effectType, newStatEffect);
        UpdateModifiedStats(effectType, newStatEffect);
    }
    
    protected PropertyInfo GetCharacterStat(CharacterModel characterModel, Stat stat) {
        var statAsString = StatToString.Map[stat];
        var characterStat = characterModel.GetType().GetProperty(statAsString);
        return characterStat;
    }

    protected void UpdateCharacterStat(PropertyInfo characterStat, CharacterModel characterModel, StatEffect statEffect) {
        var characterStatValue = GetCharacterStatValue(characterStat, statEffect.Stat);

        int newStatValue = (int)characterStatValue + statEffect.Amount;
        characterStat.SetValue(characterModel, newStatValue);
    }
    
    protected int GetCharacterStatValue(PropertyInfo characterStat, Stat stat) {
        var characterStatValue = characterStat.GetValue(Character);
        return (int) characterStatValue;
    }
    
    protected void UpdateCharacterModifiers(CharacterModel characterModel, EffectType effectType, StatEffect statEffect) {
        characterModel.StatModifiers.UpdateModifiers(effectType, statEffect);
    }
    
    protected void UpdateModifiedStats(EffectType effectType, StatEffect statEffect) {
        if (SkillEffect.StatEffectsByEffectType.ContainsKey(effectType)) {
            var statEffects = SkillEffect.StatEffectsByEffectType[effectType];
            var newStatEffect = new StatEffect(statEffect.Stat, statEffect.Amount);
            statEffects.Add(newStatEffect);
        }
        else {
            var newStatEffect = new StatEffect(statEffect.Stat, statEffect.Amount);
            var statEffects = new List<StatEffect> {newStatEffect};
            SkillEffect.StatEffectsByEffectType[effectType] = statEffects;
        }
    }
    
    
    
}