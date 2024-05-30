using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;

public class BraverySkill: DamageModifierSkill {
    public BraverySkill() : base("Bravery",
        new DamageModification(EffectType.RegularDamageIncrease, 5)
    ) {}
}