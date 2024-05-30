using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiSkills.StanceSkills;

public class SturdyStanceSkill: StanceSkill {
    
    public SturdyStanceSkill() : base("Sturdy Stance", 
        new SturdyStanceBonusSkill()
    ) {}
}