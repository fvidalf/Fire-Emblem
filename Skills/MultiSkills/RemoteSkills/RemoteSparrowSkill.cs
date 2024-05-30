using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills.RemoteBonusSkills;

namespace Fire_Emblem.Skills.MultiSkills.RemoteSkills;

public class RemoteSparrowSkill: RemoteSkill {
    
    public RemoteSparrowSkill() : base("Remote Sparrow", 
        new RemoteSparrowBonusSkill()
    ) {}
}