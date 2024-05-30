namespace Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class MagicGuardSkill: GuardSkill {
    
    public MagicGuardSkill() : base("Magic Guard") {}
    
    protected override bool IsConditionMet() {
        var doesRivalUserMagic = RoundStatus.RivalCharacterModel.Weapon == "Magic";
        return doesRivalUserMagic;
    }
}