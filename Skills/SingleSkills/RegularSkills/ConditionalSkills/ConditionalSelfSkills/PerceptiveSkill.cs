﻿using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

public class PerceptiveSkill: ConditionalSelfSkill {

    public PerceptiveSkill()
        : base(
            "Perceptive",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Spd, 1)
                }}
            }) {}

    protected override bool IsConditionMet() {
        var isFirstCharacterToAttack = Character == RoundStatus.FirstCharacterModel;
        return RoundStatus.RoundPhase == 0 && isFirstCharacterToAttack;
    }

    protected override void UpdateStat(CharacterModel characterModel, EffectType effectType, StatEffect statEffect) {
        var numberOfIncreases = Character.BaseSpd / 4;
        UpdateByVariableAmount(statEffect, numberOfIncreases);
        
        int newStatValue = (int) statEffect.Amount * numberOfIncreases + 12;
        var newStatEffect = new StatEffect(statEffect.Stat, newStatValue);
        UpdateCharacterModifiers(characterModel, effectType, newStatEffect);
        UpdateSkillEffect(effectType, newStatEffect);
    }

    private void UpdateByVariableAmount(StatEffect statEffect, int numberOfIncreases) {
        var characterStat = GetCharacterStat(Character, statEffect.Stat);
        var characterStatValue = GetCharacterStatValue(characterStat, statEffect.Stat);
        int newStatValue = (int) characterStatValue + 12;
        
        for (var spdPoint = 0; spdPoint < numberOfIncreases; spdPoint++) {
            
            newStatValue += (int) statEffect.Amount;
            characterStat.SetValue(Character, newStatValue);
        }
    }
}