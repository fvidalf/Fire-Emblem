using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.StanceSkills;

public class BracingStanceSkill: StanceSkill {

    public BracingStanceSkill() : base("Bracing Stance",
        new BracingStanceBonusSkill()
    ) {}
}