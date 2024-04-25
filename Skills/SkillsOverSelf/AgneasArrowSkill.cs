using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf;

public class AgneasArrowSkill: PenaltyNeutralizer {

    public AgneasArrowSkill()
        : base("Agnea's Arrow",
            new List<Stat> {Stat.SpdPenalty, Stat.DefPenalty, Stat.ResPenalty, Stat.HpPenalty}) {
    }
}