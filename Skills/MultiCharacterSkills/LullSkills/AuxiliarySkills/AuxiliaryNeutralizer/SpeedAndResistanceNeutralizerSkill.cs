using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryNeutralizer;

public class SpeedAndResistanceNeutralizerSkill: BonusNeutralizer {
    public SpeedAndResistanceNeutralizerSkill()
        : base("Spd/Res Neutralizer",
            new List<Stat> {Stat.SpdBonus, Stat.ResBonus}) {}
}