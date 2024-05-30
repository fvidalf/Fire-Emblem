using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.StanceSkills;

public class DartingStanceSkill: StanceSkill {
    
    public DartingStanceSkill() : base("Darting Stance", 
        new DartingStanceBonusSkill()
    ) {}
}