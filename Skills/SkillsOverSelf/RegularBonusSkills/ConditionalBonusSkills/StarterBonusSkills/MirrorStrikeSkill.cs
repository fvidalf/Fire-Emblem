using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.StarterBonusSkills;

public class MirrorStrikeSkill: StarterBonusSkill {
    
    public MirrorStrikeSkill()
        : base(
            "Sturdy Blow",
            new Dictionary<Stat, int> { { Stat.Atk, 6 }, { Stat.Res, 6 } }) { }
}