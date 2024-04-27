using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills;
using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryNeutralizer;
using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryPenalties;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public class LullSpeedAndDefenceSkill: LullSkill {
    public LullSpeedAndDefenceSkill():
        base(
            "Lull Spd/Def",
            new SpeedAndDefencePenaltySkill(),
            new SpeedAndDefenceNeutralizerSkill()
        ) {}
}