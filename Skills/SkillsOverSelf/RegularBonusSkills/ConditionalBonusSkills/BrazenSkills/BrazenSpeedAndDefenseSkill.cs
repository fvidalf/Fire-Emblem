using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.BrazenSkills;

public class BrazenSpeedAndDefenseSkill: BrazenSkill {
    
    public BrazenSpeedAndDefenseSkill() 
        :base (
            "Brazen Spd/Def",
            new Dictionary<Stat, int> { { Stat.Spd, 10 }, { Stat.Def, 10 }, }) { }
}