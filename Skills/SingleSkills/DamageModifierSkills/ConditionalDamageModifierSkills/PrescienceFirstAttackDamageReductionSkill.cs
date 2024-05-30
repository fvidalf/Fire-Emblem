using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class PrescienceFirstAttackDamageReductionSkill: ConditionalDamageModifierSkill {
    
    public PrescienceFirstAttackDamageReductionSkill() : base("Prescience First Attack Damage Reduction", 
        new DamageModification(EffectType.FirstAttackDamagePercentageReduction, 0.3)
    ) {}
    
    protected override bool IsConditionMet() {
        var isActivatingFirstToAttack = RoundStatus.ActivatingCharacterModel == RoundStatus.FirstCharacterModel;
        var isRivalBowOrMagicUser = RoundStatus.RivalCharacterModel.Weapon is "Bow" or "Magic";
        return isActivatingFirstToAttack || isRivalBowOrMagicUser;
    }
}