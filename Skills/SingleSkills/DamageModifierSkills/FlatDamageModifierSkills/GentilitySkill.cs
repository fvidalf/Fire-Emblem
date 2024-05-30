﻿using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;

public class GentilitySkill: DamageModifierSkill {
    public GentilitySkill() : base("Gentility",
        new DamageModification(EffectType.RegularDamageAbsoluteReduction, 5)
    ) {}
}