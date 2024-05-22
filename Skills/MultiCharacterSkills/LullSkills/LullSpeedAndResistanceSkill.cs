using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills;
using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryNeutralizer;
using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryPenalties;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public class LullSpeedAndResistanceSkill: LullSkill {
    public LullSpeedAndResistanceSkill():
        base(
            "Lull Spd/Res",
            new SpeedAndResistancePenaltySkill(),
            new SpeedAndResistanceNeutralizerSkill()
        ) {}
}