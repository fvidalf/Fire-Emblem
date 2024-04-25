using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.BrazenSkills;

public class BrazenSpeedAndResistanceSkill: BrazenSkill {
    
    public BrazenSpeedAndResistanceSkill() 
        :base (
            "Brazen Spd/Res",
            new Dictionary<Stat, int> { { Stat.Spd, 10 }, { Stat.Res, 10 }, }) { }
}