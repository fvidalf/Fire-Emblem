﻿using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.FixedAmountSkills.FixedAmountHybridSkills;
public class SolidGroundSkill: FixedAmountSkill {
    public SolidGroundSkill()
        : base(
            "Solid Ground", 
            new Dictionary<Stat, int> {
                {Stat.Atk, 6}, 
                {Stat.Def, 6},
                {Stat.Res, -5}
            }
        ) {}
}