﻿using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills.RemoteBonusSkills;

public class RemoteMirrorBonusSkill: StarterSkill {
    
    public RemoteMirrorBonusSkill()
        : base(
            "Remote Mirror Bonus Skill",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 7),
                    new StatEffect(Stat.Res, 10)
                }}
            }) {}
}