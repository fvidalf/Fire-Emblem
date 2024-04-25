using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.BonusSkills;

public class EarthBoostSkill: BonusSkill {
        
    public EarthBoostSkill()
        : base(
            "Earth Boost", 
            new Dictionary<Stat, int> { { Stat.Def, 6 }, }) {}
        
}