namespace Fire_Emblem.Skills.SingleSkills.CombatHpModificationSkills;

public class ResonancePreCombatHpModification : PreCombatHpModificationSkill {
    
    public ResonancePreCombatHpModification()
        : base("Resonance Hp Modification", -1) {}

    protected override bool IsConditionMet() {
        var isCharacterMagicUser = Character.Weapon == "Magic";
        var isHpGreaterOrAt2 = Character.Hp >= 2;
        return isCharacterMagicUser && isHpGreaterOrAt2;
    }
    
}