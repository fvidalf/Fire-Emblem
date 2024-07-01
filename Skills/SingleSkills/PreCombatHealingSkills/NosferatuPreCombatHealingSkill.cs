namespace Fire_Emblem.Skills.SingleSkills.PreCombatHealingSkills;

public class NosferatuPreCombatHealingSkill : PreCombatHealingSkill {
        
    public NosferatuPreCombatHealingSkill()
        : base("Nosferatu", 0.5) {}

    protected override bool IsConditionMet() {
        var isCharacterMagicUser = Character.Weapon == "Magic";
        return isCharacterMagicUser;
    }

}