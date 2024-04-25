using System.Reflection;
using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills;

// Conditional Bonus: A regular bonus skill with a condition that must be met before the bonus is applied.
public abstract class ConditionalBonusSkill: RegularBonusSkill {
    
    protected ConditionalBonusSkill(string name, Dictionary<Stat, int> statsToModify)
        : base(name, statsToModify) {}
    
    protected override void ConcreteApply(GameStatus gameStatus) {
        if (IsConditionMet()) {
            foreach (KeyValuePair<Stat, int> stat in StatsToModify) {
                Console.WriteLine("Stat: " + stat.Key);
                Console.WriteLine("Value: " + stat.Value);
                UpdateCharacterStat(stat);
            }
        }
        SetModifiedStats();
    }
    
    protected abstract bool IsConditionMet();
    
    protected PropertyInfo GetCharacterStat(Stat stat) {
        var statAsString = StatToString.Map[stat];
        var characterStat = Character.GetType().GetProperty(statAsString);
        if (characterStat is null) throw new InvalidOperationException();
        return characterStat;
    }

    protected virtual void UpdateCharacterStat(KeyValuePair<Stat, int> stat) {
        var characterStat = GetCharacterStat(stat.Key);
        var characterStatValue = GetCharacterStatValue(characterStat, stat.Key);
        
        int newStatValue = (int) characterStatValue + stat.Value;
        characterStat.SetValue(Character, newStatValue);
        
        UpdateModifiedStats(stat);
    }
}