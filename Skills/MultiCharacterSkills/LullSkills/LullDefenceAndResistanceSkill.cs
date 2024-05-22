using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills;
using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryNeutralizer;
using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryPenalties;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public class LullDefenceAndResistanceSkill: LullSkill {
    public LullDefenceAndResistanceSkill():
        base(
            "Lull Def/Res",
            new DefenceAndResistancePenaltySkill(),
            new DefenceAndResistanceNeutralizerSkill()
        ) {}
}