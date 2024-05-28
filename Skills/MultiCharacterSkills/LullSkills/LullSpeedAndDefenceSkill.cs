using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public class LullSpeedAndDefenceSkill: LullSkill {
    public LullSpeedAndDefenceSkill():
        base(
            "Lull Spd/Def",
            new SpeedAndDefencePenaltySkill(),
            new SpeedAndDefenceNeutralizerSkill()
        ) {}
}