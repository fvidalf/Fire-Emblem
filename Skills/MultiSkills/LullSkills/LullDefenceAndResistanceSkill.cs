using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

namespace Fire_Emblem.Skills.MultiSkills.LullSkills;

public class LullDefenceAndResistanceSkill: LullSkill {
    public LullDefenceAndResistanceSkill():
        base(
            "Lull Def/Res",
            new DefenceAndResistancePenaltySkill(),
            new DefenceAndResistanceNeutralizerSkill()
        ) {}
}