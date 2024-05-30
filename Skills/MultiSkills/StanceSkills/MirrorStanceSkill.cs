using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiSkills.StanceSkills;

public class MirrorStanceSkill : StanceSkill {

    public MirrorStanceSkill() : base("Mirror Stance",
        new MirrorStanceBonusSkill()
    ) {}
}