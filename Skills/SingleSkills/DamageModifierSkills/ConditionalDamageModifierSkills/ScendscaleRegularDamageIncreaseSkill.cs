using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class ScendscaleRegularDamageIncreaseSkill : ConditionalDamageModifierSkill {

    public ScendscaleRegularDamageIncreaseSkill(): base("Scendscale Regular Damage Increase",
        new DamageModification(EffectType.RegularDamageIncrease, 0)
    ) {}
    
    public override void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        DetermineTarget();
        SetDamageIncrease();
        if (IsConditionMet()) {
            Character.UpdateDamageModifiers(DamageModification);
        }
    }
    
    protected override bool IsConditionMet() {
        return true;
    }
    
    private void SetDamageIncrease() {
        var damageIncrease = RoundStatus.ActivatingCharacterModel.Atk * 0.25;
        DamageModification.Amount = (int) damageIncrease;
    }
}