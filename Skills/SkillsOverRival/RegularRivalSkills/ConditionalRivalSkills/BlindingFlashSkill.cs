﻿using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverRival.RegularRivalSkills.ConditionalRivalSkills;

public class BlindingFlashSkill: ConditionalRivalSkill {
    public BlindingFlashSkill()
        : base("Blinding Flash", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularPenalty, new List<StatEffect> {
                new StatEffect(Stat.Spd, -4),
            }}
        }) {}

    protected override bool IsConditionMet() {
        var IsFirstToAttack = GameStatus.ActivatingCharacter == GameStatus.FirstCharacter;
        return IsFirstToAttack;
    }
}