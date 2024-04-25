using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.ConditionalBonusSkills.StarterBonusSkills;

public class DeathBlowSkill: StarterBonusSkill {
    
    public DeathBlowSkill()
        : base(
            "Death Blow",
            new Dictionary<Stat, int> { { Stat.Atk, 8 }, }) { }
}