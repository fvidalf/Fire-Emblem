using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.FlatSelfSkills.SacrificeSkills;

public class FortifyDefenseAndResistanceSelfSkill : FlatSelfSkill {
    public FortifyDefenseAndResistanceSelfSkill()
        : base("Fort. Def/Res", new Dictionary<EffectType, List<StatEffect>>()) {

        StatsToModify[EffectType.RegularBonus] = new List<StatEffect> {
            new StatEffect(Stat.Def, 6),
            new StatEffect(Stat.Res, 6)
        };
        
        StatsToModify[EffectType.RegularPenalty] = new List<StatEffect> {
            new StatEffect(Stat.Atk, -2),
        };
    }
}