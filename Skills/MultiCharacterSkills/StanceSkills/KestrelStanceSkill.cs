using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.RivalStarterSkills.StanceBonusSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.StanceSkills;

public class KestrelStanceSkill: StanceSkill {
    
    public KestrelStanceSkill() : base("Kestrel Stance", 
        new KestrelStanceBonusSkill()
    ) {}
}