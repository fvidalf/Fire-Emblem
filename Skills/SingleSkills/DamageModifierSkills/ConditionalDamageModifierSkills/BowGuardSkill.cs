﻿namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class BowGuardSkill: GuardSkill {
    public BowGuardSkill() : base("Bow Guard") {}
    
    protected override bool IsConditionMet() {
        var doesRivalUseBow = RoundStatus.RivalCharacterModel.Weapon == "Bow";
        return doesRivalUseBow;
    }
}