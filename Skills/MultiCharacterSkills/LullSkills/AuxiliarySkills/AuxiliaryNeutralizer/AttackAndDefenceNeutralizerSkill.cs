using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;

namespace Fire_Emblem.Skills.MultiCharacterSkills.LullSkills.AuxiliarySkills.AuxiliaryNeutralizer;

public class AttackAndDefenceNeutralizerSkill: BonusNeutralizer {
    public AttackAndDefenceNeutralizerSkill()
        : base("Atk/Def Neutralizer",
            new List<Stat> {Stat.AtkBonus, Stat.DefBonus}) {}
}