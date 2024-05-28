using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public class LullSpeedAndResistanceSkill: LullSkill {
    public LullSpeedAndResistanceSkill():
        base(
            "Lull Spd/Res",
            new SpeedAndResistancePenaltySkill(),
            new SpeedAndResistanceNeutralizerSkill()
        ) {}
}