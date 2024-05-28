using Fire_Emblem.CharacterFiles.StatFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;

public class AttackAndDefenceNeutralizerSkill: BonusNeutralizer {
    public AttackAndDefenceNeutralizerSkill()
        : base("Atk/Def Neutralizer",
            new List<Stat> {Stat.AtkBonus, Stat.DefBonus}) {}
}