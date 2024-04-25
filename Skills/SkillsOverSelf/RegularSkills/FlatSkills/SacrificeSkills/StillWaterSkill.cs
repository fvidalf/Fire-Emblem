﻿using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.FlatSkills.SacrificeSkills;

public class StillWaterSkill : FlatSkill {
    public StillWaterSkill()
        : base("Still Water", new Dictionary<EffectType, List<StatEffect>>()) {

        StatsToModify[EffectType.RegularBonus] = new List<StatEffect> {
            new StatEffect(Stat.Atk, 6),
            new StatEffect(Stat.Res, 6)
        };
        
        StatsToModify[EffectType.RegularPenalty] = new List<StatEffect> {
            new StatEffect(Stat.Def, -5),
        };
    }
}