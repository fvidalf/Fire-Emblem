namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class AxeGuardSkill: GuardSkill {
    public AxeGuardSkill() : base("Axe Guard") {}

    protected override bool IsConditionMet() {
        var doesRivalUseBow = RoundStatus.RivalCharacterModel.Weapon == "Axe";
        return doesRivalUseBow;
    }
}