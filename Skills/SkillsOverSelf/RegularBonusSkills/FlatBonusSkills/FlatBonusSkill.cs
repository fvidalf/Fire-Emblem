using System.Reflection;
using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.FlatBonusSkills;

// Flat Bonus: A regular bonus skill with no conditions for being applied. 
public abstract class FlatBonusSkill: RegularBonusSkill {
    
    protected FlatBonusSkill(string name, Dictionary<Stat, int> statsToModify)
    : base(name, statsToModify) {}
    
    protected override void ConcreteApply(GameStatus gameStatus) {
        foreach (KeyValuePair<Stat, int> stat in StatsToModify) {
            UpdateCharacterStat(Character, stat);
        }
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
        var characterStatValue = GetCharacterStatValue(characterStat, stat.Key);
        
        int newStatValue = (int) characterStatValue + stat.Value;
        characterStat.SetValue(character, newStatValue);
        
        UpdateModifiedStats(stat);
    }
}