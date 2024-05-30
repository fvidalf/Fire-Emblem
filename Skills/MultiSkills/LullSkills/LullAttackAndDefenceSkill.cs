using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

namespace Fire_Emblem.Skills.MultiSkills.LullSkills;

public class LullAttackAndDefenceSkill: LullSkill {
    public LullAttackAndDefenceSkill():
        base(
            "Lull Atk/Def",
            new AttackAndDefencePenaltySkill(),
            new AttackAndDefenceNeutralizerSkill()
        ) {}
}