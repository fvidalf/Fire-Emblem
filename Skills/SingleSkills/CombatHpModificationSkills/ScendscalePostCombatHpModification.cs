namespace Fire_Emblem.Skills.SingleSkills.CombatHpModificationSkills;

public class ScendscalePostCombatHpModification : PostCombatHpModification{
    
    public ScendscalePostCombatHpModification()
        : base("Scendscale Hp Modification", -7) {}

    protected override bool IsConditionMet() {
        var hasAttackedAtLeastOnce = Character.HasAttackedAtLeastOnce;
        return hasAttackedAtLeastOnce;
    }
}