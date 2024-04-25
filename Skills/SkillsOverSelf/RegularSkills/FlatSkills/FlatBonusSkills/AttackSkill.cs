using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.FlatSkills.FlatBonusSkills;

public class AttackSkill: FlatSkill {
    public AttackSkill()
        : base("Attack +6", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 6)
                }}
        }) {}
}