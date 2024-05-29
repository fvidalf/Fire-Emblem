using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatRivalSkills.LullPenalties;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public class LullAttackAndSpeedSkill: LullSkill {
    public LullAttackAndSpeedSkill():
        base(
            "Lull Atk/Spd",
            new AttackAndSpeedPenaltySkill(),
            new AttackAndSpeedNeutralizerSkill()
        ) {}
}