﻿using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.FlatSkills.FlatBonusSkills;

public class SpeedAndResistanceSkill: FlatSkill {
    public SpeedAndResistanceSkill()
        : base("Spd/Res +5", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                new StatEffect(Stat.Spd, 5),
                new StatEffect(Stat.Res, 5)
            }}
        }) {}
}