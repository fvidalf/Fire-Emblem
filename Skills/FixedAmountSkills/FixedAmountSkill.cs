using System.Reflection;
using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.FixedAmountSkills;

// FixedAmountBonus: A skill that increases a character's stat(s) by a fixed amount
public abstract class FixedAmountSkill: IBaseSkill{
    public string Name { get; set; }
    private Dictionary<Stat, int> _statsToModify;
    private Dictionary<Stat, int> _modifiedStats;
    
    protected FixedAmountSkill(string name, Dictionary<Stat, int> statsToModify) {
        Name = name;
        _statsToModify = statsToModify;
        _modifiedStats = new Dictionary<Stat, int>();
    }

    public void Apply(GameStatus gameStatus) {
        var character = gameStatus.AttackingCharacter;
        foreach (KeyValuePair<Stat, int> stat in _statsToModify) {
            var characterStat = GetCharacterStat(character, stat.Key);
            if (characterStat != null) {
                var characterStatValue = (int) (characterStat.GetValue(character) ?? 0);
                int modifiedStat = characterStatValue + stat.Value;
                characterStat.SetValue(character, modifiedStat);
                _modifiedStats.Add(stat.Key, modifiedStat);
            }
        }
    }
    
    private PropertyInfo? GetCharacterStat(Character character, Stat stat) {
        var statAsString = StatToString.Map[stat];
        var characterStat = character.GetType().GetProperty(statAsString);
        return characterStat;
    }
    
    public Dictionary<Stat, int> GetModifiedStats() {
        return _modifiedStats;
    }
}