﻿using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

public abstract class ConditionalSelfSkill: ConditionalSkill {
    
    protected ConditionalSelfSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name, statsToModify) {}
    
    public override void DetermineTarget() {
        Character = RoundStatus.ActivatingCharacterModel;
    }
}