using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryNeutralizer;

public class AttackAndSpeedNeutralizerSkill: BonusNeutralizer {
    public AttackAndSpeedNeutralizerSkill()
        : base("Atk/Spd Neutralizer",
            new List<Stat> {Stat.AtkBonus, Stat.SpdBonus}) {}
}