using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiSkills.StanceSkills;

public class KestrelStanceSkill: StanceSkill {
    
    public KestrelStanceSkill() : base("Kestrel Stance", 
        new KestrelStanceBonusSkill()
    ) {}
}