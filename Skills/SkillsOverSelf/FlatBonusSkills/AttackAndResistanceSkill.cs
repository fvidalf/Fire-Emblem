using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.FlatBonusSkills;

public class AttackAndResistanceSkill: FlatBonusSkill {
    public AttackAndResistanceSkill()
        : base("Atk/Res +5", new Dictionary<Stat, int> {{Stat.Atk, 5}, {Stat.Res, 5}}) {}
}