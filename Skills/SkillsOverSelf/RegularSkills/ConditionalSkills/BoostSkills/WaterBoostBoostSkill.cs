﻿using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.ConditionalSkills.BoostSkills;

public class WaterBoostBoostSkill: BoostSkill {
            
    public WaterBoostBoostSkill()
        : base(
            "Water Boost", 
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Res, 6)
                }}
            }) {}
}