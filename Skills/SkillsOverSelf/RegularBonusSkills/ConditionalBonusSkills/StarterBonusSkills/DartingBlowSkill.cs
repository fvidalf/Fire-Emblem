using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.StarterBonusSkills;

public class DartingBlowSkill: StarterBonusSkill {
    
    public DartingBlowSkill()
        : base(
            "Darting Blow",
            new Dictionary<Stat, int> { { Stat.Spd, 8 }, }) { }
}