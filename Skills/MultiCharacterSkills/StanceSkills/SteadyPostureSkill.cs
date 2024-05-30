using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.StanceSkills;

public class SteadyPostureSkill: StanceSkill {
    
    public SteadyPostureSkill() : base("Steady Posture", 
        new SteadyPostureBonusSkill()
    ) {}
}