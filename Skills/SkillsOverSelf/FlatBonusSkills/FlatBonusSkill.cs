using System.Reflection;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.FlatBonusSkills;

public abstract class FlatBonusSkill: SkillOverSelf {
    
    protected Dictionary<Stat, int> StatsToModify;
    protected Dictionary<Stat, int> ModifiedStats;
    
    protected FlatBonusSkill(string name, Dictionary<Stat, int> statsToModify)
    : base(name) {
        StatsToModify = statsToModify;
        ModifiedStats = new Dictionary<Stat, int>();
    }
    
    protected override void ConcreteApply(Character character) {
        foreach (KeyValuePair<Stat, int> stat in StatsToModify) {
            UpdateCharacterStat(character, stat);
        }
        SetEffectType();
        SetModifiedStats();
    }
    
    private PropertyInfo GetCharacterStat(Character character, Stat stat) {
        var statAsString = StatToString.Map[stat];
        var characterStat = character.GetType().GetProperty(statAsString);
        if (characterStat is null) throw new InvalidOperationException();
        return characterStat;
    }
    
    private void UpdateCharacterStat(Character character, KeyValuePair<Stat, int> stat) {
        var characterStat = GetCharacterStat(character, stat.Key);
        var characterStatValue = GetCharacterStatValue(character, characterStat, stat.Key);
        
        int newStatValue = (int) characterStatValue + stat.Value;
        characterStat.SetValue(character, newStatValue);
        
        UpdateModifiedStats(stat);
    }
    
    private int GetCharacterStatValue(Character character, PropertyInfo characterStat, Stat stat) {
        var characterStatValue = characterStat.GetValue(character);
        if (characterStatValue is null) throw new InvalidOperationException();
        return (int) characterStatValue;
    }

    private void UpdateModifiedStats(KeyValuePair<Stat, int> stat) {
        if (ModifiedStats.ContainsKey(stat.Key)) {
            ModifiedStats[stat.Key] += stat.Value;
        }
        else {
            ModifiedStats[stat.Key] = stat.Value;
        }
    }

    protected override void SetEffectType() {
        SkillEffect.EffectType = EffectType.RegularBonus;
    }
    
    protected void SetModifiedStats() {
        SkillEffect.Stats = ModifiedStats;
    }
}