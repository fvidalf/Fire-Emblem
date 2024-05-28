using Fire_Emblem.CharacterFiles.StatFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.PenaltyNeutralizers;

public class AgneasArrowSkill: PenaltyNeutralizer {

    public AgneasArrowSkill()
        : base("Agnea's Arrow",
            new List<Stat> {Stat.AtkPenalty, Stat.SpdPenalty, Stat.DefPenalty, Stat.ResPenalty}) {
    }
}