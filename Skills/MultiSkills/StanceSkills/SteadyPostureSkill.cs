using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiSkills.StanceSkills;

public class SteadyPostureSkill: StanceSkill {
    
    public SteadyPostureSkill() : base("Steady Posture", 
        new SteadyPostureBonusSkill()
    ) {}
}