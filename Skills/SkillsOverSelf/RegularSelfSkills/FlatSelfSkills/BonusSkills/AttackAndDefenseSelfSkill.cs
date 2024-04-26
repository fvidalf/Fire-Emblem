using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.FlatSelfSkills.BonusSkills;

public class AttackAndDefenseSelfSkill: FlatSelfSkill {
    public AttackAndDefenseSelfSkill()
        : base("Atk/Def +5", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                new StatEffect(Stat.Atk, 5),
                new StatEffect(Stat.Def, 5)
            }}
        }) {}
}