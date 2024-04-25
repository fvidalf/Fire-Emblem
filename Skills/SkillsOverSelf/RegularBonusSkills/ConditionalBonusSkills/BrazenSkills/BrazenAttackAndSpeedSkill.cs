using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.BrazenSkills;

public class BrazenAttackAndSpeedSkill: BrazenSkill {
    
    public BrazenAttackAndSpeedSkill() 
        :base (
            "Brazen Atk/Spd",
            new Dictionary<Stat, int> { { Stat.Atk, 10 }, { Stat.Spd, 10 }, }) { }
}