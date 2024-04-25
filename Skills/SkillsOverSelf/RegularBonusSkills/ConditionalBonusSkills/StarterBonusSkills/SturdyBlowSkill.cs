using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.StarterBonusSkills;

public class SturdyBlowSkill: StarterBonusSkill {
    
    public SturdyBlowSkill()
        : base(
            "Sturdy Blow",
            new Dictionary<Stat, int> { { Stat.Atk, 6 }, { Stat.Def, 6 } }) { }
}