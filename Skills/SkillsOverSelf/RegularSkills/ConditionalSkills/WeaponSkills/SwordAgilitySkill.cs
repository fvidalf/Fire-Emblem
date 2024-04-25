﻿using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;
using Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.ConditionalSkills.BrazenSkills;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.ConditionalSkills.WeaponSkills;

public class SwordAgilitySkill : WeaponSkill {

    public SwordAgilitySkill()
        : base(
            "Sword Agility",
            new Dictionary<EffectType, List<StatEffect>>(),
            "Sword") {
        
        StatsToModify[EffectType.RegularBonus] = new List<StatEffect> {
            new StatEffect(Stat.Spd, 12),
        };
        
        StatsToModify[EffectType.RegularPenalty] = new List<StatEffect> {
            new StatEffect(Stat.Atk, -6),
        };
    }
}