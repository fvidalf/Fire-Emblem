using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.FixedAmountSkills.FixedAmountBonusSkills;

public class SpeedSkill: FixedAmountSkill{
    public SpeedSkill()
        : base("Speed +5", new Dictionary<Stat, int> {{Stat.Spd, 5}}) {}
}