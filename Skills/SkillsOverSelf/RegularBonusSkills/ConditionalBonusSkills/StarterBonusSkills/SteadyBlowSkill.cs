using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.StarterBonusSkills;

public class SteadyBlowSkill: StarterBonusSkill {
    
    public SteadyBlowSkill()
        : base(
            "Steady Blow",
            new Dictionary<Stat, int> { { Stat.Spd, 6 }, { Stat.Def, 6 } }) { }
}