using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.StarterBonusSkills;

public class BracingBlowSkill: StarterBonusSkill {
    
    public BracingBlowSkill()
        : base(
            "Bracing Blow",
            new Dictionary<Stat, int> { { Stat.Def, 6 }, { Stat.Res, 6 } }) { }
}