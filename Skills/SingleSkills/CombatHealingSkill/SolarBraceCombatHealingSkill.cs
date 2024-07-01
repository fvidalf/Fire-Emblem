namespace Fire_Emblem.Skills.SingleSkills.CombatHealingSkill;

public class SolarBraceCombatHealingSkill : CombatHealingSkill {
        
    public SolarBraceCombatHealingSkill()
        : base("Solar Brace", 0.5) {}

    protected override bool IsConditionMet() {
        var isCharacterFirstToAttack = RoundStatus.ActivatingCharacterModel == RoundStatus.FirstCharacterModel;
        return isCharacterFirstToAttack;
    }
}