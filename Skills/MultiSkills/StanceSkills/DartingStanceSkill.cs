using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiSkills.StanceSkills;

public class DartingStanceSkill: StanceSkill {
    
    public DartingStanceSkill() : base("Darting Stance", 
        new DartingStanceBonusSkill()
    ) {}
}