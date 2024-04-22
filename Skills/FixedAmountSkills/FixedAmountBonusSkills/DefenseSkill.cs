using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.FixedAmountBonusSkills;

namespace Fire_Emblem.Skills.FixedAmountSkills.FixedAmountBonusSkills;

public class DefenseSkill: FixedAmountSkill {
    public DefenseSkill()
        : base("Defense +5", new Dictionary<Stat, int> {{Stat.Def, 5}}) {}
}