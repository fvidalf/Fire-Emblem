using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.BrazenSkills;

public class BrazenDefenseAndResistanceSkill : BrazenSkill {

    public BrazenDefenseAndResistanceSkill()
        : base(
            "Brazen Def/Res",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Def, 10),
                    new StatEffect(Stat.Res, 10)
                }}
            }) {}
}