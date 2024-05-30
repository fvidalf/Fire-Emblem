namespace Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class LanceGuardSkill: GuardSkill {
    
    public LanceGuardSkill() : base("Lance Guard") {}
    
    protected override bool IsConditionMet() {
        var doesRivalUseLance = RoundStatus.RivalCharacterModel.Weapon == "Lance";
        return doesRivalUseLance;
    }
}