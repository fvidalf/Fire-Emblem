using Fire_Emblem.CharacterFiles.StatFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;

public class AttackAndSpeedNeutralizerSkill: BonusNeutralizer {
    public AttackAndSpeedNeutralizerSkill()
        : base("Atk/Spd Neutralizer",
            new List<Stat> {Stat.AtkBonus, Stat.SpdBonus}) {}
}