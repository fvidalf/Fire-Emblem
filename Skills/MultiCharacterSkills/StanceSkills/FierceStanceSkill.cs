using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.StanceSkills;

public class FierceStanceSkill: StanceSkill {
    
    public FierceStanceSkill() : base("Fierce Stance", 
        new FierceStanceBonusSkill()
    ) {}
}