﻿using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills;

public class BracingBlowSkill: StarterSkill {
    
    public BracingBlowSkill()
        : base(
            "Bracing Blow",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {{
                    new StatEffect(Stat.Def, 6)},
                    new StatEffect(Stat.Res, 6)
                    }}
            }) {}
}