using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatSelfSkills.BonusSkills;

public class AttackAndResistanceSkill: FlatSelfSkill {
    public AttackAndResistanceSkill()
        : base("Atk/Res +5", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                new StatEffect(Stat.Atk, 5),
                new StatEffect(Stat.Res, 5)
            }}
        }) {}
}