using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

namespace Fire_Emblem.Skills.MultiSkills.LullSkills;

public class LullSpeedAndResistanceSkill: LullSkill {
    public LullSpeedAndResistanceSkill():
        base(
            "Lull Spd/Res",
            new SpeedAndResistancePenaltySkill(),
            new SpeedAndResistanceNeutralizerSkill()
        ) {}
}