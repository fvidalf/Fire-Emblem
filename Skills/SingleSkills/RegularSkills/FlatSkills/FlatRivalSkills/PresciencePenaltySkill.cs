﻿using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills;

public class PresciencePenaltySkill: FlatRivalSkill {
    public PresciencePenaltySkill()
        : base("Prescience Penalty", new Dictionary<EffectType, List<StatEffect>> {
                {
                    EffectType.RegularPenalty, new List<StatEffect> {
                        new StatEffect(Stat.Atk, -5),
                        new StatEffect(Stat.Res, -5)
                    }
                }
            }
        ) {}
}