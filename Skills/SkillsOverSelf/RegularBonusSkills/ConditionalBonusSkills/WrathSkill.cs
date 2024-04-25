using System.Reflection;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills;

public class WrathSkill: ConditionalBonusSkill {

    public WrathSkill()
        : base(
            "Wrath",
            new Dictionary<Stat, int> { { Stat.Atk, 1 }, { Stat.Spd, 1 }, }) { }

    protected override bool IsConditionMet() {
        return GameStatus.RoundPhase == 0;
    }

    protected override void UpdateCharacterStat(KeyValuePair<Stat, int> stat) {
        var diffInHp = Character.BaseHp - Character.Hp;
        var numberOfIncreases = Math.Min(diffInHp, 30);
        UpdateByVariableAmount(stat, numberOfIncreases);
        
        var newStat = new KeyValuePair<Stat, int>(stat.Key, stat.Value * numberOfIncreases);
        UpdateModifiedStats(newStat);
    }

    private void UpdateByVariableAmount(KeyValuePair<Stat, int> stat, int numberOfIncreases) {
        var characterStat = GetCharacterStat(stat.Key);
        var characterStatValue = GetCharacterStatValue(characterStat, stat.Key);
        int newStatValue = (int)characterStatValue;
        
        for (var hpPoint = 0; hpPoint < numberOfIncreases; hpPoint++) {
            
            newStatValue += (int) stat.Value;
            characterStat.SetValue(Character, newStatValue);
        }
    }
}