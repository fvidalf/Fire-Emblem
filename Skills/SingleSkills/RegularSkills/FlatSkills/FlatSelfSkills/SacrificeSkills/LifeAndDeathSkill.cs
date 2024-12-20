﻿using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatSelfSkills.SacrificeSkills;

public class LifeAndDeathSkill : FlatSelfSkill {
    public LifeAndDeathSkill()
        : base("Life and Death", new Dictionary<EffectType, List<StatEffect>>()) {

        StatsToModify[EffectType.RegularBonus] = new List<StatEffect> {
            new StatEffect(Stat.Atk, 6),
            new StatEffect(Stat.Spd, 6)
        };
        
        StatsToModify[EffectType.RegularPenalty] = new List<StatEffect> {
            new StatEffect(Stat.Def, -5),
            new StatEffect(Stat.Res, -5)
        };
    }
}




