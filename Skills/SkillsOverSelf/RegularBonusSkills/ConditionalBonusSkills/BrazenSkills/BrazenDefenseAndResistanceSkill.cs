using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.BrazenSkills;

public class BrazenDefenseAndResistanceSkill: BrazenSkill {
    
    public BrazenDefenseAndResistanceSkill() 
        :base (
            "Brazen Def/Res",
            new Dictionary<Stat, int> { { Stat.Def, 10 }, { Stat.Res, 10 }, }) { }
}