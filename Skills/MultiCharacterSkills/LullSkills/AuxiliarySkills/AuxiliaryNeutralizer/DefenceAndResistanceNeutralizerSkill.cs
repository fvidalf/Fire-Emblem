using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryNeutralizer;

public class DefenceAndResistanceNeutralizerSkill: BonusNeutralizer {
    public DefenceAndResistanceNeutralizerSkill()
        : base("Def/Res Neutralizer",
            new List<Stat> {Stat.DefBonus, Stat.ResBonus}) {}
}