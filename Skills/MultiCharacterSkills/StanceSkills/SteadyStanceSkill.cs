using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.StanceSkills;

public class SteadyStanceSkill: StanceSkill {
    public SteadyStanceSkill() : base("Steady Stance", 
        new SteadyStanceBonusSkill()
    ) {}
}