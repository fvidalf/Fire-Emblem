using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatSelfSkills.BonusSkills;

public class FuryBonusSkill: FlatSelfSkill {
    public FuryBonusSkill()
        : base("Fury", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                new StatEffect(Stat.Atk, 4),
                new StatEffect(Stat.Spd, 4),
                new StatEffect(Stat.Def, 4),
                new StatEffect(Stat.Res, 4)
            }}
        }) {}
}