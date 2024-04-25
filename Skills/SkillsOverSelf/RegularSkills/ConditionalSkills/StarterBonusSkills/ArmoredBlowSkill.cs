using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.ConditionalSkills.StarterBonusSkills;

public class ArmoredBlowSkill: StarterSkill {
    
    public ArmoredBlowSkill()
        : base(
            "Armored Blow",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Def, 8)
                }}
            }) {}
}