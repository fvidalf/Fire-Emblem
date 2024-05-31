using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;

public class GuardBearingRegularDamageReductionSkill: DamageModifierSkill {
    
    public GuardBearingRegularDamageReductionSkill()
        : base("Guard Bearing: Regular Damage Reduction",
            new DamageModification(EffectType.RegularDamagePercentageReduction, 0)
        ) {
    }
    
    public override void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        DetermineTarget();
        SetDamageReduction();
        Character.UpdateDamageModifiers(DamageModification);
    }
    
    private void SetDamageReduction() {
        var activatingCharacter = RoundStatus.ActivatingCharacterModel;
        var isUnitFirstToAttack = activatingCharacter == RoundStatus.FirstCharacterModel;
        
        if (!activatingCharacter.HasUsedGuardBearingForDefending && !isUnitFirstToAttack) {
            activatingCharacter.HasUsedGuardBearingForDefending = true;
            DamageModification.SetAmount(0.6);
        } else if (!activatingCharacter.HasUsedGuardBearingForAttacking && isUnitFirstToAttack) {
            activatingCharacter.HasUsedGuardBearingForAttacking = true;
            DamageModification.SetAmount(0.6);
        } else {
            DamageModification.SetAmount(0.3);
        }
    }
    
}