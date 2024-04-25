using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSkills.StarterBonusSkills;

public class WardingBlowSkill: StarterSkill {
    
    public WardingBlowSkill()
        : base(
            "Warding Blow",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Res, 8)
                }}
            }) {}
}