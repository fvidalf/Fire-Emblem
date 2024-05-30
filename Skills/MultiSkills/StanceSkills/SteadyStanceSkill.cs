using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiSkills.StanceSkills;

public class SteadyStanceSkill: StanceSkill {
    public SteadyStanceSkill() : base("Steady Stance", 
        new SteadyStanceBonusSkill()
    ) {}
}