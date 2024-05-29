using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public class LullAttackAndResistanceSkill: LullSkill {
    public LullAttackAndResistanceSkill():
        base(
            "Lull Atk/Spd",
            new AttackAndResistancePenaltySkill(),
            new AttackAndResistanceNeutralizerSkill()
        ) {}
}