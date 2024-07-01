using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.CombatHpModificationSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills;
namespace Fire_Emblem.Skills.MultiSkills;

public class MysticBoostSkill: MultiSkill {

    public MysticBoostSkill()
        : base("Mystic Boost", new ISingleSkill[] {
            new MysticBoostPenaltySkill(),
            new PostCombatHpModification("Mystic Boost", 10)
        }) {}
}
