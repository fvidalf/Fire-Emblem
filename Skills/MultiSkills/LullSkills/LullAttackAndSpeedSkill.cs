using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

namespace Fire_Emblem.Skills.MultiSkills.LullSkills;

public class LullAttackAndSpeedSkill: LullSkill {
    public LullAttackAndSpeedSkill():
        base(
            "Lull Atk/Spd",
            new AttackAndSpeedPenaltySkill(),
            new AttackAndSpeedNeutralizerSkill()
        ) {}
}