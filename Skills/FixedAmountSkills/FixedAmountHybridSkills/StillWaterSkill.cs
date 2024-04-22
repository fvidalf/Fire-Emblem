﻿using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.FixedAmountBonusSkills;

namespace Fire_Emblem.Skills.FixedAmountSkills.FixedAmountHybridSkills;
public class StillWaterSkill: FixedAmountSkill {
    public StillWaterSkill()
        : base(
            "Still Water", 
            new Dictionary<Stat, int> {
                {Stat.Atk, 6}, 
                {Stat.Res, 6},
                {Stat.Def, -5},
            }
        ) {}
}