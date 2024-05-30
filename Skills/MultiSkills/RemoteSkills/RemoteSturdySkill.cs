using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills.RemoteBonusSkills;

namespace Fire_Emblem.Skills.MultiSkills.RemoteSkills;

public class RemoteSturdySkill: RemoteSkill {
    
    public RemoteSturdySkill() : base("Remote Sturdy", 
        new RemoteSturdyBonusSkill()
    ) {}
}