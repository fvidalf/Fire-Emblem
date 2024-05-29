using Fire_Emblem.CharacterFiles.StatFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;

public class AttackAndResistanceNeutralizerSkill: BonusNeutralizer {
    public AttackAndResistanceNeutralizerSkill()
        : base("Atk/Res Neutralizer",
            new List<Stat> {Stat.AtkBonus, Stat.ResBonus}) {}
}