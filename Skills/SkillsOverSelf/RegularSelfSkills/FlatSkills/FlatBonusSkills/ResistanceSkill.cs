using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.FlatSkills.FlatBonusSkills;

public class ResistanceSkill: FlatSkill {
    public ResistanceSkill()
        : base("Resistance +5", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                new StatEffect(Stat.Res, 5)
            }}
        }) {}
}