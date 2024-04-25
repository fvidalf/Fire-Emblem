using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.StarterBonusSkills;

public class SwiftStrikeSkill: StarterBonusSkill {
    
    public SwiftStrikeSkill()
        : base(
            "Swift Strike",
            new Dictionary<Stat, int> { { Stat.Spd, 6 }, { Stat.Res, 6 } }) { }
}