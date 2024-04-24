using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.FlatBonusSkills;

public class SpeedSkill: FlatBonusSkill{
    public SpeedSkill()
        : base("Speed +5", new Dictionary<Stat, int> {{Stat.Spd, 5}}) {}
}