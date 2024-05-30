using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.StanceSkills;

public class SwiftStanceSkill: StanceSkill {

    public SwiftStanceSkill() : base("Swift Stance",
        new SwiftStanceBonusSkill()
    ) {}
}