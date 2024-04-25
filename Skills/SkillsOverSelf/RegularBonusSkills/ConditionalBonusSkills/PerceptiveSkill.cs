using System.Reflection;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills;

public class PerceptiveSkill: ConditionalBonusSkill {

    public PerceptiveSkill()
        : base(
            "Perceptive",
            new Dictionary<Stat, int> { { Stat.Spd, 1 }, }) { }

    protected override bool IsConditionMet() {
        var isFirstCharacterToAttack = Character == GameStatus.FirstCharacter;
        return GameStatus.RoundPhase == 0 && isFirstCharacterToAttack;
    }

    protected override void UpdateCharacterStat(KeyValuePair<Stat, int> stat) {
        var numberOfIncreases = Character.BaseSpd / 4;
        UpdateByVariableAmount(stat, numberOfIncreases);
        
        var newStat = new KeyValuePair<Stat, int>(stat.Key, stat.Value * numberOfIncreases + 12);
        UpdateModifiedStats(newStat);
    }

    private void UpdateByVariableAmount(KeyValuePair<Stat, int> stat, int numberOfIncreases) {
        var characterStat = GetCharacterStat(stat.Key);
        var characterStatValue = GetCharacterStatValue(characterStat, stat.Key);
        int newStatValue = (int) characterStatValue + 12;
        
        for (var spdPoint = 0; spdPoint < numberOfIncreases; spdPoint++) {
            
            newStatValue += (int) stat.Value;
            characterStat.SetValue(Character, newStatValue);
        }
    }
}