using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.BonusSkills;

public class FireBoostSkill: BonusSkill {
        
    public FireBoostSkill()
        : base(
            "Fire Boost", 
            new Dictionary<Stat, int> { { Stat.Atk, 6 }, }) {}
    
}