using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatSelfSkills.SacrificeSkills;

public class FortifyDefenseAndResistanceSkill : FlatSelfSkill {
    public FortifyDefenseAndResistanceSkill()
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