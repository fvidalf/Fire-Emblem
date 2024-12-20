﻿using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

public class WillToWinSkill: ConditionalSelfSkill {
    
    public WillToWinSkill()
        : base(
            "Will to Win", 
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 8)
                }}
            }) {}

    protected override bool IsConditionMet() {
        var isHpAtOrBelowHalf = Character.Hp <= Character.BaseHp / 2;
        return isHpAtOrBelowHalf;
    }
}