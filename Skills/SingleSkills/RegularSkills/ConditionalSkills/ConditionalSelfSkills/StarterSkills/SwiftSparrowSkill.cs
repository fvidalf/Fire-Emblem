﻿using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills;

public class SwiftSparrowSkill: StarterSkill {
    
    public SwiftSparrowSkill()
        : base(
            "Swift Sparrow",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 6),
                    new StatEffect(Stat.Spd, 6)
                }}
            }) {}
}