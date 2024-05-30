using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills.RemoteBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.RemoteSkills;

public class RemoteSparrowSkill: RemoteSkill {
    
    public RemoteSparrowSkill() : base("Remote Sparrow", 
        new RemoteSparrowBonusSkill()
    ) {}
}