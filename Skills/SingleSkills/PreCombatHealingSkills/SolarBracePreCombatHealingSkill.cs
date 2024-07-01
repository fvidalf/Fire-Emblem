namespace Fire_Emblem.Skills.SingleSkills.PreCombatHealingSkills;

public class SolarBracePreCombatHealingSkill : PreCombatHealingSkill {
    
    public SolarBracePreCombatHealingSkill()
        : base("Solar Brace", 0.5) {}

    protected override bool IsConditionMet() {
        var isCharacterFirstToAttack = RoundStatus.ActivatingCharacterModel == RoundStatus.FirstCharacterModel;
        return isCharacterFirstToAttack;
    }
}