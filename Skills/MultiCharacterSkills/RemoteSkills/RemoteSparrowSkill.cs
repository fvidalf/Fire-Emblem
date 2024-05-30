using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterBonusSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterBonusSkills.RemoteBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.RemoteSkills;

public class RemoteSparrowSkill: RemoteSkill {
    
    public RemoteSparrowSkill() : base("Remote Sparrow", 
        new RemoteSparrowBonusSkill()
    ) {}
}