using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.BonusSkills;

public class WaterBoostSkill: BonusSkill {
            
    public WaterBoostSkill()
        : base(
            "Water Boost", 
            new Dictionary<Stat, int> { { Stat.Res, 6 }, }) {}
}