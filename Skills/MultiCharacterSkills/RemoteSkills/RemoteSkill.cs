﻿using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.RemoteSkills;

public abstract class RemoteSkill: MultiCharacterSkill {
    
    public RemoteSkill(string name, StatModifierSkill statBonusSkill)
        : base(name, new ISingleCharacterSkill[] {statBonusSkill, new RemoteFirstAttackDamageReductionSkill()})
    {}
    
}