using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.BonusSkills;

public class WindBoostSkill: BonusSkill {
        
    public WindBoostSkill()
        : base(
            "Wind Boost", 
            new Dictionary<Stat, int> { { Stat.Spd, 6 }, }) {}
        
}