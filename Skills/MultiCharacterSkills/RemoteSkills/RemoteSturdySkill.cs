using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills.RemoteBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.RemoteSkills;

public class RemoteSturdySkill: RemoteSkill {
    
    public RemoteSturdySkill() : base("Remote Sturdy", 
        new RemoteSturdyBonusSkill()
    ) {}
}