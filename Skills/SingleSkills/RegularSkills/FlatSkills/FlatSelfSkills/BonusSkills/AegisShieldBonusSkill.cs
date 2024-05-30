using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatSelfSkills.BonusSkills;

public class AegisShieldBonusSkill: FlatSelfSkill {
    
    public AegisShieldBonusSkill()
        : base("Aegis Shield Bonus Skill", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                new StatEffect(Stat.Def, 6),
                new StatEffect(Stat.Res, 3)
            }}
        }) {}
}