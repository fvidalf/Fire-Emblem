using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryNeutralizer;

public class AttackAndResistanceNeutralizerSkill: BonusNeutralizer {
    public AttackAndResistanceNeutralizerSkill()
        : base("Atk/Res Neutralizer",
            new List<Stat> {Stat.AtkBonus, Stat.ResBonus}) {}
}