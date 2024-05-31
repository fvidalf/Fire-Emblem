using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class DivineRecreationSkill: MultiSkill {
    public DivineRecreationSkill()
        : base("Divine Recreation", new ISingleSkill[] {
            new DivineRecreationPenaltySkill(),
            new DivineRecreationFirstAttackDamageReductionSkill(),
            new DivineRecreationNextAttackDamageIncreaseSkill()
        }) {}
}