using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;

public class AegisShieldFirstAttackDamageReductionSkill: DamageModifierSkill {
    
    public AegisShieldFirstAttackDamageReductionSkill()
        : base("Aegis Shield First Attack Damage Reduction",
            new DamageModification(EffectType.FirstAttackDamagePercentageReduction, 0.5)
        ) {
    }
}