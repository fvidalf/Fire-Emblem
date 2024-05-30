﻿using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class PoeticJusticeSkill: MultiSkill {
    
    public PoeticJusticeSkill()
        : base("Poetic Justice", new ISingleSkill[] {
            new PoeticJusticePenaltySkill(),
            new PoeticJusticeRegularDamageIncreaseSkill()
        }) {}
    
}