using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillEffectFiles;

public class SkillEffect
{
    public Dictionary<EffectType, List<StatEffect>> StatEffectsByEffectType { get; set; }
    
    public SkillEffect() {
        StatEffectsByEffectType = new Dictionary<EffectType, List<StatEffect>>();
    }
    
    public void Join(SkillEffect newSkillEffect) {
        foreach (var statEffectsByEffectType in newSkillEffect.StatEffectsByEffectType) {
            var foundEffectType = false;
            var newEffectType = statEffectsByEffectType.Key;
            var newStatEffects = statEffectsByEffectType.Value;
            if (this.StatEffectsByEffectType.ContainsKey(newEffectType)) {
                foundEffectType = true;
                // Now, check if each of the new stats is in the old list of statEffects
                foreach (var newStatEffect in newStatEffects) {
                    var oldStatEffects = this.StatEffectsByEffectType[newEffectType];
                    var foundStat = false;
                    foreach (var oldStatEffect in oldStatEffects) {
                        if (oldStatEffect.Stat == newStatEffect.Stat) {
                            foundStat = true;
                            oldStatEffect.Amount += newStatEffect.Amount;
                        }
                    }

                    if (!foundStat) oldStatEffects.Add(newStatEffect);
                }
            }
            else {
                this.StatEffectsByEffectType[newEffectType] = newStatEffects;
            }
        } 
    }

    public List<Tuple<EffectType, Stat, int>> CollapseIntoList() {
        var collapsedList = new List<Tuple<EffectType, Stat, int>>();
        foreach (var statEffectsByEffectType in StatEffectsByEffectType) {
            var effectType = statEffectsByEffectType.Key;
            var statEffects = statEffectsByEffectType.Value;
            foreach (var statEffect in statEffects) {
                collapsedList.Add(new Tuple<EffectType, Stat, int>(effectType, statEffect.Stat, statEffect.Amount));
            }
        }
        return collapsedList;
    }
    
    public void ConsoleWriteStats() {
        foreach (var statEffectsByEffectType in StatEffectsByEffectType) {
            var effectType = statEffectsByEffectType.Key;
            var statEffects = statEffectsByEffectType.Value;
            foreach (var statEffect in statEffects) {
                Console.WriteLine($"{effectType}: {statEffect.Stat} +{statEffect.Amount}");
            }
        }
    }
}