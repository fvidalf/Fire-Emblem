using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.StanceSkills;

public class WardingStanceSkill: StanceSkill {
    
    public WardingStanceSkill() : base("Warding Stance", 
        new WardingStanceBonusSkill()
    ) {}
}