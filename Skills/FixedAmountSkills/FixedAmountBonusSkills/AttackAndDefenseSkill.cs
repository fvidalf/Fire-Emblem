﻿using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.FixedAmountSkills.FixedAmountBonusSkills;

public class AttackAndDefenseSkill: FixedAmountSkill {
    public AttackAndDefenseSkill()
        : base("Atk/Def +5", new Dictionary<Stat, int> {{Stat.Atk, 5}, {Stat.Def, 5}}) {}
}