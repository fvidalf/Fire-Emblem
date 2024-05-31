using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class GuardBearingSkill: MultiSkill {
    
    public GuardBearingSkill()
        : base("Guard Bearing", new ISingleSkill[] {
            new GuardBearingPenaltySkill(),
            new GuardBearingRegularDamageReductionSkill()
        }) {}
    
}