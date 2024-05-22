using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.RegularSkillTypes;

public abstract class FlatSkill: RegularSkill, ITargetedSkill {
    
    protected FlatSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
    : base(name, statsToModify) {}
    
    protected override void ConcreteApply(GameStatus gameStatus) {
        DetermineTarget(gameStatus);
        foreach (KeyValuePair<EffectType, List<StatEffect>> statEffects in StatsToModify) {
            var effectType = statEffects.Key;
            foreach (var statEffect in statEffects.Value) {
                UpdateStat(Character, effectType, statEffect);
            }
        }
    }

    public abstract void DetermineTarget(GameStatus GameStatus);
}