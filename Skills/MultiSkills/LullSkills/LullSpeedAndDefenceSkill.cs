using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

namespace Fire_Emblem.Skills.MultiSkills.LullSkills;

public class LullSpeedAndDefenceSkill: LullSkill {
    public LullSpeedAndDefenceSkill():
        base(
            "Lull Spd/Def",
            new SpeedAndDefencePenaltySkill(),
            new SpeedAndDefenceNeutralizerSkill()
        ) {}
}