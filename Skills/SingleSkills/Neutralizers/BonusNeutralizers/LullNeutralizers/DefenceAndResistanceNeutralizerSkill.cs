using Fire_Emblem.CharacterFiles.StatFiles;

namespace Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers.LullNeutralizers;

public class DefenceAndResistanceNeutralizerSkill: BonusNeutralizer {
    public DefenceAndResistanceNeutralizerSkill()
        : base("Def/Res Neutralizer",
            new List<Stat> {Stat.DefBonus, Stat.ResBonus}) {}
}