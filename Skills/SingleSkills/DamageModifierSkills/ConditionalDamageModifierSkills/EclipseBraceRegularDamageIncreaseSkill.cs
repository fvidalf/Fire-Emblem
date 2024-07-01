using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class EclipseBraceRegularDamageIncreaseSkill: ConditionalDamageModifierSkill {

    public EclipseBraceRegularDamageIncreaseSkill(): base("Eclipse Brace Regular Damage Increase",
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
        var isActivatingFirstToAttack = RoundStatus.ActivatingCharacterModel == RoundStatus.FirstCharacterModel;
        var isActivatingPhysical = RoundStatus.ActivatingCharacterModel.IsPhysical();
        return isActivatingFirstToAttack && isActivatingPhysical;
    }
    
    private void SetDamageIncrease() {
        var damageIncrease = RoundStatus.RivalCharacterModel.Def * 0.3;
        DamageModification.Amount = (int) damageIncrease;
    }
}