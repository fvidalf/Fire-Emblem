using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class ExtraChivalrySkill: MultiSkill {
    
    public ExtraChivalrySkill()
        : base("Extra Chivalry", new ISingleSkill[] {
            new ExtraChivalryPenaltySkill(),
            new ExtraChivalryRegularDamagePercentageReductionSkill()
        }) {}
}