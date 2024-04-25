using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.FlatBonusSkills;

public class DefenseSkill: FlatBonusSkill {
    public DefenseSkill()
        : base("Defense +5", new Dictionary<Stat, int> {{Stat.Def, 5}}) {}
}