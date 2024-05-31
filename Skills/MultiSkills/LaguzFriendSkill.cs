using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatRivalSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatSelfSkills.SacrificeSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class LaguzFriendSkill: MultiSkill {
    
    public LaguzFriendSkill()
        : base("Laguz Friend", new ISingleSkill[] {
            new LaguzFriendRegularDamageReductionSkill(),
            new LaguzFriendNeutralizerSkill(),
            new LaguzFriendPenaltySkill()
        }) {}
    
}