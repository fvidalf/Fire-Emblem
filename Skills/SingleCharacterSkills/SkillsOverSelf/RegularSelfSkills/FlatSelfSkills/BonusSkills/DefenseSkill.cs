using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverSelf.RegularSelfSkills.FlatSelfSkills.BonusSkills;

public class DefenseSkill : FlatSelfSkill {
    public DefenseSkill()
        : base("Defense +5", new Dictionary<EffectType, List<StatEffect>> {
            {
                EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Def, 5)
                }
            }
        }) {}
}