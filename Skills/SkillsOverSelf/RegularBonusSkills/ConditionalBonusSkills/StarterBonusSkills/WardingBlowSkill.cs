using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.StarterBonusSkills;

public class WardingBlowSkill: StarterBonusSkill {
    
    public WardingBlowSkill()
        : base(
            "Warding Blow",
            new Dictionary<Stat, int> { { Stat.Res, 8 }, }) { }
}