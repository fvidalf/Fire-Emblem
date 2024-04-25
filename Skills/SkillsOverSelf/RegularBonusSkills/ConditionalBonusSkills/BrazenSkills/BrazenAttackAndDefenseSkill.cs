using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.BrazenSkills;

public class BrazenAttackAndDefenseSkill: BrazenSkill {
    
    public BrazenAttackAndDefenseSkill() 
        :base (
            "Brazen Atk/Def",
            new Dictionary<Stat, int> { { Stat.Atk, 10 }, { Stat.Def, 10 }, }) { }
}