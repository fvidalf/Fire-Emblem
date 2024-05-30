﻿using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills.RemoteBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.RemoteSkills;

public class RemoteMirrorSkill: RemoteSkill {
    
    public RemoteMirrorSkill() : base("Remote Sparrow", 
        new RemoteMirrorBonusSkill()
    ) {}
}