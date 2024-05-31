using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class MoonTwinWingSkill: MultiSkill {
    
    public MoonTwinWingSkill()
        : base("Moon-Twin Wing", new ISingleSkill[] {
            new MoonTwinWingRegularDamageReductionSkill(),
            new MoonTwinWingPenaltySkill()
        }) {}
    
}