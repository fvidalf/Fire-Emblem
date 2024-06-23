using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles.SortedEffectsFiles;

namespace Fire_Emblem.Skills.SkillEffectFiles;

public class SkillEffect
{
    public Dictionary<EffectType, List<StatEffect>> StatEffectsByEffectType { get; }
    
    public SkillEffect() {
        StatEffectsByEffectType = new Dictionary<EffectType, List<StatEffect>>();
    }
    
    public void Join(SkillEffect newSkillEffect) {
        foreach (var statEffectsByEffectType in newSkillEffect.StatEffectsByEffectType) {
            var newEffectType = statEffectsByEffectType.Key;
            var newStatEffects = statEffectsByEffectType.Value;
            AddNewStatEffects(newEffectType, newStatEffects);
        } 
    }

    private void AddNewStatEffects(EffectType newEffectType, List<StatEffect> newStatEffects) {
        if (StatEffectsByEffectType.ContainsKey(newEffectType)) {
            foreach (var newStatEffect in newStatEffects) {
                AddNewStatEffectToExistingKey(newEffectType, newStatEffect);
            }
        } else {
            StatEffectsByEffectType[newEffectType] = newStatEffects;
        }
    }
    
    private void AddNewStatEffectToExistingKey(EffectType newEffectType, StatEffect newStatEffect) {
        var oldStatEffects = StatEffectsByEffectType[newEffectType];
        var foundStat = false;
                    
        foreach (var oldStatEffect in oldStatEffects) {
            if (oldStatEffect.Stat == newStatEffect.Stat) {
                foundStat = true;
                oldStatEffect.Amount += newStatEffect.Amount;
            }
        }
        if (!foundStat) oldStatEffects.Add(newStatEffect);
    }
    
    public SortedEffects GetSortedEffects() {
        var simpleEffects = CollapseIntoList();
        var sortedEffects = new SortedEffects(simpleEffects);
        return sortedEffects;
    }

    private List<SimpleEffect> CollapseIntoList() {
        var collapsedList = new List<SimpleEffect>();
        foreach (var statEffectsByEffectType in StatEffectsByEffectType) {
            AddStatEffectsToList(collapsedList, statEffectsByEffectType);
        }
        return collapsedList;
    }

    private void AddStatEffectsToList(List<SimpleEffect> collapsedList, KeyValuePair<EffectType, List<StatEffect>> statEffectsByEffectType) {
        var effectType = statEffectsByEffectType.Key;
        var statEffects = statEffectsByEffectType.Value;

        foreach (var statEffect in statEffects) {
            collapsedList.Add(new SimpleEffect(effectType, statEffect.Stat, statEffect.Amount));
        }
        if (statEffects.Count == 0) AddDummyStatEffectToList(collapsedList, effectType);
    }

    private void AddDummyStatEffectToList(List<SimpleEffect> collapsedList, EffectType effectType) {
        collapsedList.Add(new SimpleEffect(effectType, Stat.Null, 0));
    }
}