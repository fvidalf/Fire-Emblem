﻿using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

public class SteadyStanceBonusSkill: RivalStarterSkill {
    
    public SteadyStanceBonusSkill()
        : base(
            "Steady Stance Bonus Skill",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Def, 8)
                }}
            }) {}
}