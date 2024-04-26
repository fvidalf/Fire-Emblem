using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.FlatSelfSkills.SacrificeSkills;

public class LifeAndDeathSelfSkill : FlatSelfSkill {
    public LifeAndDeathSelfSkill()
        : base("Life and Death", new Dictionary<EffectType, List<StatEffect>>()) {

        StatsToModify[EffectType.RegularBonus] = new List<StatEffect> {
            new StatEffect(Stat.Atk, 6),
            new StatEffect(Stat.Spd, 6)
        };
        
        StatsToModify[EffectType.RegularPenalty] = new List<StatEffect> {
            new StatEffect(Stat.Def, -5),
            new StatEffect(Stat.Res, -5)
        };
    }
}




