using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.FixedAmountBonusSkills;

namespace Fire_Emblem.Skills.FixedAmountSkills.FixedAmountBonusSkills;

public class AttackSkill: FixedAmountSkill {
    public AttackSkill()
        : base("Attack +6", new Dictionary<Stat, int> {{Stat.Atk, 6}}) {}
}