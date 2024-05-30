using Fire_Emblem.CharacterFiles.StatFiles;

namespace Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;

public class SpeedAndResistanceNeutralizerSkill: BonusNeutralizer {
    public SpeedAndResistanceNeutralizerSkill()
        : base("Spd/Res Neutralizer",
            new List<Stat> {Stat.SpdBonus, Stat.ResBonus}) {}
}