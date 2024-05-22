﻿using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.StarterBonusSkills;

public class DartingBlowSkill : StarterSkill {

    public DartingBlowSkill()
        : base(
            "Darting Blow",
            new Dictionary<EffectType, List<StatEffect>> {
                {
                    EffectType.RegularBonus, new List<StatEffect> {
                        new StatEffect(Stat.Spd, 8)
                    }
                }
            }) { }
}