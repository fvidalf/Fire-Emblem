using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class PrescienceSkill: MultiSkill {
    
    public PrescienceSkill()
        : base("Prescience", new ISingleSkill[] {
            new PresciencePenaltySkill(),
            new PrescienceFirstAttackDamageReductionSkill()
        }
    ) {}
    
}