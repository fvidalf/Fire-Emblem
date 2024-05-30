using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatSelfSkills.BonusSkills;

public class AttackSkill: FlatSelfSkill {
    public AttackSkill()
        : base("Attack +6", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 6)
                }}
        }) {}
}