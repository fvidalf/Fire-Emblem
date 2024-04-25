using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.StarterBonusSkills;

public class ArmoredBlowSkill: StarterBonusSkill {
    
    public ArmoredBlowSkill()
        : base(
            "Armored Blow",
            new Dictionary<Stat, int> { { Stat.Def, 8 }, }) { }
}