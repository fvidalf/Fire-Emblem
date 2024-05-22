using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills;
using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryNeutralizer;
using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryPenalties;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public class LullAttackAndResistanceSkill: LullSkill {
    public LullAttackAndResistanceSkill():
        base(
            "Lull Atk/Spd",
            new AttackAndResistancePenaltySkill(),
            new AttackAndResistanceNeutralizerSkill()
        ) {}
}