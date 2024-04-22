using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.FixedAmountBonusSkills;

namespace Fire_Emblem.Skills.FixedAmountSkills.FixedAmountBonusSkills;

public class AttackAndResistanceSkill: FixedAmountSkill {
    public AttackAndResistanceSkill()
        : base("Atk/Res +5", new Dictionary<Stat, int> {{Stat.Atk, 5}, {Stat.Res, 5}}) {}
}