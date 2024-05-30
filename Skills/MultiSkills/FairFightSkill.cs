using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class FairFightSkill: MultiSkill {
    
    public FairFightSkill()
        : base("Light and Dark", new StatModifierSkill[] {
            new FairFightSelfBonusSkill(),
            new FairFightRivalBonusSkill()
        }) {
    }
}