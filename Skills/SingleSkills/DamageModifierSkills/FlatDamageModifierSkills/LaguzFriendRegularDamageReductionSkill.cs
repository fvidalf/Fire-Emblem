using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;

public class LaguzFriendRegularDamageReductionSkill: DamageModifierSkill {
    
    public LaguzFriendRegularDamageReductionSkill() : base("Laguz Friend: Regular Damage Reduction",
        new DamageModification(EffectType.RegularDamagePercentageReduction, 0.5)
    ) {}
}