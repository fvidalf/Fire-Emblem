using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.FlatBonusSkills;

public class AttackSkill: FlatBonusSkill {
    public AttackSkill()
        : base("Attack +6", new Dictionary<Stat, int> {{Stat.Atk, 6}}) {}
}