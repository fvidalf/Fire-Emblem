﻿using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatSelfSkills.BonusSkills;

public class DefenseSkill : FlatSelfSkill {
    public DefenseSkill()
        : base("Defense +5", new Dictionary<EffectType, List<StatEffect>> {
            {
                EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Def, 5)
                }
            }
        }) {}
}