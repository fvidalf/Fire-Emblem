using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public class LullAttackAndDefenceSkill: LullSkill {
    public LullAttackAndDefenceSkill():
        base(
            "Lull Atk/Def",
            new AttackAndDefencePenaltySkill(),
            new AttackAndDefenceNeutralizerSkill()
        ) {}
}