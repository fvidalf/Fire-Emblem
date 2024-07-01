namespace Fire_Emblem.Skills.SingleSkills.CombatHealingSkill;

public class NosferatuCombatHealingSkill : CombatHealingSkill {
        
    public NosferatuCombatHealingSkill()
        : base("Nosferatu", 0.5) {}

    protected override bool IsConditionMet() {
        var isCharacterMagicUser = Character.Weapon == "Magic";
        return isCharacterMagicUser;
    }

}