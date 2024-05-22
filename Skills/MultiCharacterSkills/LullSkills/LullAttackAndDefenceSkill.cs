using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills;
using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryNeutralizer;
using Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryPenalties;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills;

public class LullAttackAndDefenceSkill: LullSkill {
    public LullAttackAndDefenceSkill():
        base(
            "Lull Atk/Def",
            new AttackAndDefencePenaltySkill(),
            new AttackAndDefenceNeutralizerSkill()
        ) {}
}