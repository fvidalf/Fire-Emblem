using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.FixedAmountSkills.FixedAmountBonusSkills;

public class SpeedAndResistanceSkill: FixedAmountSkill {
    public SpeedAndResistanceSkill()
        : base("Spd/Res +5", new Dictionary<Stat, int> {{Stat.Spd, 5}, {Stat.Res, 5}}) {}
}