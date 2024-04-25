using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.BrazenSkills;

public class BrazenAttackAndResistanceSkill: BrazenSkill {
    
    public BrazenAttackAndResistanceSkill() 
        :base (
            "Brazen Atk/Res",
            new Dictionary<Stat, int> { { Stat.Atk, 10 }, { Stat.Res, 10 }, }) { }
}