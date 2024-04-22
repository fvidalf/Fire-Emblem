using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.FixedAmountBonusSkills;

namespace Fire_Emblem.Skills.FixedAmountSkills.FixedAmountHybridSkills;

public class FortifyDefenseAndResistanceSkill: FixedAmountSkill {
    public FortifyDefenseAndResistanceSkill()
        : base(
            "Fort. Def/Res", 
            new Dictionary<Stat, int> {
                {Stat.Def, 5}, 
                {Stat.Res, 5},
                {Stat.Atk, -2}
            }
        ) {}
}