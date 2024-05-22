using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryNeutralizer;

public class SpeedAndDefenceNeutralizerSkill: BonusNeutralizer {
    public SpeedAndDefenceNeutralizerSkill()
        : base("Spd/Def Neutralizer",
            new List<Stat> {Stat.SpdBonus, Stat.DefBonus}) {}
}