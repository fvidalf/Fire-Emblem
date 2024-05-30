using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public abstract class GuardSkill: ConditionalDamageModifierSkill {
    public GuardSkill(string name) : base(name,
        new DamageModification(EffectType.RegularDamageAbsoluteReduction, 5)
    ) {}
    
    protected abstract override bool IsConditionMet();
}