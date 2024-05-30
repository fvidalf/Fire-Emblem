using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterSkills.RemoteBonusSkills;

public class RemoteSparrowBonusSkill: StarterSkill {
    
    public RemoteSparrowBonusSkill()
        : base(
            "Remote Sparrow Bonus Skill",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 7),
                    new StatEffect(Stat.Spd, 7)
                }}
            }) {}
}