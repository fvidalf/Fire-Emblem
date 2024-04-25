using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSkills.FlatSkills;

// Flat Bonus: A regular bonus skill with no conditions for being applied. 
public abstract class FlatSkill: RegularSkill {
    
    protected FlatSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
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