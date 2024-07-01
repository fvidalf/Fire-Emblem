namespace Fire_Emblem.Skills.SingleSkills.CombatHpModificationSkills;

public class PushPostCombatHpModification : PostCombatHpModification {
    
    public PushPostCombatHpModification()
        : base("Push Hp Modification", -5) {}

    protected override bool IsConditionMet() {
        var hasAttackedAtLeastOnce = Character.HasAttackedAtLeastOnce;
        var isHpAtOrAbove25 = Character.RoundStartHp >= Character.BaseHp * 0.25;
        return hasAttackedAtLeastOnce && isHpAtOrAbove25;
    }
}