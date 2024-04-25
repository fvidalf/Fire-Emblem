using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.FlatBonusSkills;

public class AttackAndDefenseSkill: FlatBonusSkill {
    public AttackAndDefenseSkill()
        : base("Atk/Def +5", new Dictionary<Stat, int> { { Stat.Atk, 5 }, { Stat.Def, 5 } }) {}
}