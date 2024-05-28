using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

public class WrathSkill: ConditionalSelfSkill {

    public WrathSkill()
        : base(
            "Wrath",
             new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 1),
                    new StatEffect(Stat.Spd, 1)
                }}
            }) {}

    protected override bool IsConditionMet() {
        return RoundStatus.RoundPhase == 0;
    }

    protected override void UpdateStat(CharacterModel characterModel, EffectType effectType, StatEffect statEffect) {
        var diffInHp = Character.BaseHp - Character.Hp;
        var numberOfIncreases = Math.Min(diffInHp, 30);
        UpdateByVariableAmount(statEffect, numberOfIncreases);

        var newStatValue = statEffect.Amount * numberOfIncreases;
        var newStatEffect = new StatEffect(statEffect.Stat, newStatValue);
        UpdateModifiedStats(effectType, newStatEffect);
    }

    private void UpdateByVariableAmount(StatEffect statEffect, int numberOfIncreases) {
        var characterStat = GetCharacterStat(Character, statEffect.Stat);
        var characterStatValue = GetCharacterStatValue(characterStat, statEffect.Stat);
        int newStatValue = (int)characterStatValue;
        
        for (var hpPoint = 0; hpPoint < numberOfIncreases; hpPoint++) {
            
            newStatValue += (int) statEffect.Amount;
            characterStat.SetValue(Character, newStatValue);
        }
    }
}