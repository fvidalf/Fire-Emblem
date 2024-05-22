using System.ComponentModel;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;

namespace Fire_Emblem.Skills.SkillEffectFiles;

public class SkillEffect
{
    public Dictionary<EffectType, List<StatEffect>> StatEffectsByEffectType { get; set; }
    
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
        if (this.StatEffectsByEffectType.ContainsKey(newEffectType)) {
            foreach (var newStatEffect in newStatEffects) {
                AddNewStatEffectToExistingKey(newEffectType, newStatEffect);
            }
        } else {
            this.StatEffectsByEffectType[newEffectType] = newStatEffects;
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

    public List<Tuple<EffectType, Stat, int>> CollapseIntoList() {
        var collapsedList = new List<Tuple<EffectType, Stat, int>>();
        foreach (var statEffectsByEffectType in StatEffectsByEffectType) {
            AddStatEffectsToList(collapsedList, statEffectsByEffectType);
        }
        return collapsedList;
    }
    
    private void AddStatEffectsToList(List<Tuple<EffectType, Stat, int>> collapsedList, KeyValuePair<EffectType, List<StatEffect>> statEffectsByEffectType) {
        var effectType = statEffectsByEffectType.Key;
        var statEffects = statEffectsByEffectType.Value;
        
        foreach (var statEffect in statEffects) {
            collapsedList.Add(new Tuple<EffectType, Stat, int>(effectType, statEffect.Stat, statEffect.Amount));
        }
        if (statEffects.Count == 0) AddDummyStatEffectToList(collapsedList, effectType);
    }
    
    private void AddDummyStatEffectToList(List<Tuple<EffectType, Stat, int>> collapsedList, EffectType effectType) {
        collapsedList.Add(new Tuple<EffectType, Stat, int>(effectType, Stat.Null, 0));
    }
}