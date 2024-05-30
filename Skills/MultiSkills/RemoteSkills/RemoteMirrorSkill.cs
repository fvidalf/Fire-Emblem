using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills.RemoteBonusSkills;

namespace Fire_Emblem.Skills.MultiSkills.RemoteSkills;

public class RemoteMirrorSkill: RemoteSkill {
    
    public RemoteMirrorSkill() : base("Remote Sparrow", 
        new RemoteMirrorBonusSkill()
    ) {}
}