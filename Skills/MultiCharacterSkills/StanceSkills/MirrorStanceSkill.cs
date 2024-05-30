using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.StanceSkills;

public class MirrorStanceSkill : StanceSkill {

    public MirrorStanceSkill() : base("Mirror Stance",
        new MirrorStanceBonusSkill()
    ) {}
}