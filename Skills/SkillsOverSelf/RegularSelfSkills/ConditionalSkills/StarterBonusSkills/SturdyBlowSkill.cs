using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSkills.StarterBonusSkills;

public class SturdyBlowSkill: StarterSkill {
    
    public SturdyBlowSkill()
        : base(
            "Sturdy Blow",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 6),
                    new StatEffect(Stat.Def, 6)
                }}
            }) {}
}