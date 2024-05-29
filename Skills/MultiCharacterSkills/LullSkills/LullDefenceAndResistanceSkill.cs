using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public class LullDefenceAndResistanceSkill: LullSkill {
    public LullDefenceAndResistanceSkill():
        base(
            "Lull Def/Res",
            new DefenceAndResistancePenaltySkill(),
            new DefenceAndResistanceNeutralizerSkill()
        ) {}
}