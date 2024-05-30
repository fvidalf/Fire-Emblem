using Fire_Emblem.Skills.SingleSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class LaguzFriendSkill: MultiSkill {
    
    public LaguzFriendSkill()
        : base("Laguz Friend", new ISingleSkill[] {
            /*
            new LaguzFriendRegularPercentageDamageReductionSkill(),
            new LaguzFriendNeutralizerSkill(),
            new LaguzFriendPenaltySkill()
            */
        }) {}
    
}