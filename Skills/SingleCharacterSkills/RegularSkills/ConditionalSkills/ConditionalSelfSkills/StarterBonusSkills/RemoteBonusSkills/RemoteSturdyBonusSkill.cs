using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterBonusSkills.RemoteBonusSkills;

public class RemoteSturdyBonusSkill: StarterSkill {
    
    public RemoteSturdyBonusSkill()
        : base(
            "Remote Sturdy Bonus Skill",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 7),
                    new StatEffect(Stat.Def, 10)
                }}
            }) {}
}