using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.FlatSkills.FlatSelfSkills.SacrificeSkills;

public class LaguzFriendPenaltySkill: FlatSelfSkill {
    
    public LaguzFriendPenaltySkill()
        : base("Laguz Friend: Penalty", new Dictionary<EffectType, List<StatEffect>> {}) {}
    
    protected override void ConcreteApply(RoundStatus roundStatus) {
        DetermineTarget();
        SetStatsToModify();
        foreach (KeyValuePair<EffectType, List<StatEffect>> statEffects in StatsToModify) {
            var effectType = statEffects.Key;
            foreach (var statEffect in statEffects.Value) {
                UpdateStat(Character, effectType, statEffect);
            }
        }
    }

    private void SetStatsToModify() {
        var baseDef = RoundStatus.ActivatingCharacterModel.BaseDef;
        var baseRes = RoundStatus.ActivatingCharacterModel.BaseRes;
        var defReduction = baseDef / 2;
        var resReduction = baseRes / 2;
        
        StatsToModify = new Dictionary<EffectType, List<StatEffect>> {
            {
                EffectType.RegularPenalty, new List<StatEffect> {
                    new StatEffect(Stat.Def, -defReduction),
                    new StatEffect(Stat.Res, -resReduction)
                }
            }
        };
    }
}