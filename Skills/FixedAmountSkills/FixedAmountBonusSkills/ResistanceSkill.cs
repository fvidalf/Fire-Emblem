﻿using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.FixedAmountSkills.FixedAmountBonusSkills;

public class ResistanceSkill: FixedAmountSkill {
    public ResistanceSkill()
        : base("Resistance +5", new Dictionary<Stat, int> {{Stat.Res, 5}}) {}
}