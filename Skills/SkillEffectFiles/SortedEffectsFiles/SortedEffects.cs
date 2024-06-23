using System.Collections;
namespace Fire_Emblem.Skills.SkillEffectFiles.SortedEffectsFiles;

public class SortedEffects : IEnumerable<SimpleEffect> {

    private List<SimpleEffect> _effects;

    public SortedEffects(List<SimpleEffect> sortedEffects) {
        _effects = sortedEffects;
        SortEffects();
    }

    private void SortEffects() {
        var effectsSortedByEffectType = _effects.OrderBy(effect => effect.Effect);
        var effectsSortedByStat = effectsSortedByEffectType.ThenBy(effect => effect.Stat);
        _effects = effectsSortedByStat.ToList();
    }
    
    public IEnumerator<SimpleEffect> GetEnumerator() {
        return _effects.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return GetEnumerator();
    }
}