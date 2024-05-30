using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiSkills.StanceSkills;

public class WardingStanceSkill: StanceSkill {
    
    public WardingStanceSkill() : base("Warding Stance", 
        new WardingStanceBonusSkill()
    ) {}
}