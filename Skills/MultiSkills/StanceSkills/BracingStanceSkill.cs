using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiSkills.StanceSkills;

public class BracingStanceSkill: StanceSkill {

    public BracingStanceSkill() : base("Bracing Stance",
        new BracingStanceBonusSkill()
    ) {}
}