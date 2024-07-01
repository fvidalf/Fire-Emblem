using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.CombatHpModificationSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatSelfSkills.BonusSkills;
namespace Fire_Emblem.Skills.MultiSkills;

public class FurySkill: MultiSkill {

    public FurySkill()
        : base("Fury", new ISingleSkill[] {
            new FuryBonusSkill(),
            new PostCombatHpModification("Fury", -8)
        }) {}
}
