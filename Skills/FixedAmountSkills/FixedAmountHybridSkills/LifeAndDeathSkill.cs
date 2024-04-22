using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.FixedAmountSkills.FixedAmountHybridSkills;
public class LifeAndDeathSkill: FixedAmountSkill {
    public LifeAndDeathSkill()
        : base(
            "Life and Death", 
            new Dictionary<Stat, int> {
                {Stat.Atk, 6}, 
                {Stat.Spd, 6},
                {Stat.Def, -5},
                {Stat.Res, -5}
            }
        ) {}
}