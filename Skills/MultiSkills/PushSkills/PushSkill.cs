using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.CombatHpModificationSkills;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.PushBonusSkills;
namespace Fire_Emblem.Skills.MultiSkills.PushSkills;

public abstract class PushSkill : MultiSkill {
    
    public PushSkill(Stat firstStat, Stat secondStat)
        : base("Push", new ISingleSkill[] {
            new PushBonusSkill(firstStat, secondStat),
            new PushPostCombatHpModification()
        }) {
    }
}