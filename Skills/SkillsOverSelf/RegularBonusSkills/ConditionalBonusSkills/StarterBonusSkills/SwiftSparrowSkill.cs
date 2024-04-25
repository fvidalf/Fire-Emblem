using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.StarterBonusSkills;

public class SwiftSparrowSkill: StarterBonusSkill {
    
    public SwiftSparrowSkill()
        : base(
            "Swift Sparrow",
            new Dictionary<Stat, int> { { Stat.Atk, 6 }, { Stat.Spd, 6} }) { }
}