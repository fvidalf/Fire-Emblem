using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;

public class DragonsWrathFirstAttackDamageReductionSkill: DamageModifierSkill {
    public DragonsWrathFirstAttackDamageReductionSkill()
        : base("Dragon's Wrath: First Attack Damage Reduction", 
            new DamageModification(EffectType.FirstAttackDamagePercentageReduction, 0.25)
        ) {}
}