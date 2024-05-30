using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

namespace Fire_Emblem.Skills.MultiSkills.LullSkills;

public class LullAttackAndResistanceSkill: LullSkill {
    public LullAttackAndResistanceSkill():
        base(
            "Lull Atk/Spd",
            new AttackAndResistancePenaltySkill(),
            new AttackAndResistanceNeutralizerSkill()
        ) {}
}