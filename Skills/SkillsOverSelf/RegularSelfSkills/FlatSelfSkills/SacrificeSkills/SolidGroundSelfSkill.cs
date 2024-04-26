using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.FlatSelfSkills.SacrificeSkills;

public class SolidGroundSelfSkill : FlatSelfSkill {
    public SolidGroundSelfSkill()
        : base("Solid Ground", new Dictionary<EffectType, List<StatEffect>>()) {

        StatsToModify[EffectType.RegularBonus] = new List<StatEffect> {
            new StatEffect(Stat.Atk, 6),
            new StatEffect(Stat.Def, 6)
        };
        
        StatsToModify[EffectType.RegularPenalty] = new List<StatEffect> {
            new StatEffect(Stat.Res, -5)
        };
    }
}