using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills;
using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryNeutralizer;
using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryPenalties;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public class LullAttackAndSpeedSkill: LullSkill {
    public LullAttackAndSpeedSkill():
        base(
            "Lull Atk/Spd",
            new AttackAndSpeedPenaltySkill(),
            new AttackAndSpeedNeutralizerSkill()
        ) {}
}