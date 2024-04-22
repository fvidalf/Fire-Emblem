using Fire_Emblem.CharacterFiles;
namespace Fire_Emblem.Skills.FixedAmountSkills.FixedAmountBonusSkills;

public class AttackSkill: FixedAmountSkill {
    public AttackSkill()
        : base("Attack +6", new Dictionary<Stat, int> {{Stat.Atk, 6}}) {}
}