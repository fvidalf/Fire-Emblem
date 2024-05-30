using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.StanceSkills;

public class SturdyStanceSkill: StanceSkill {
    
    public SturdyStanceSkill() : base("Sturdy Stance", 
        new SturdyStanceBonusSkill()
    ) {}
}