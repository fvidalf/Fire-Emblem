using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverRival.RegularRivalSkills.FlatRivalSkills;

public abstract class FlatRivalSkill: RegularRivalSkill {
    
    protected FlatRivalSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name, statsToModify) {}
    
    protected override void ConcreteApply(GameStatus gameStatus) {
        foreach (KeyValuePair<EffectType, List<StatEffect>> statEffects in StatsToModify) {
            var effectType = statEffects.Key;
            foreach (var statEffect in statEffects.Value) {
                UpdateCharacterStat(Character, effectType, statEffect);
            }
        }
    }
}