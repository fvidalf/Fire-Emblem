using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.DamageModifiersFiles;

public class DamageModifiers {
    public Dictionary<EffectType, double> DamageModifiersByEffectType { get; set; }
    
    public DamageModifiers() {
        DamageModifiersByEffectType = new Dictionary<EffectType, double>();
    }
    
    public void UpdateDamageModifier(DamageModification damageModification) {
        var effectType = damageModification.EffectType;
        var amount = damageModification.Amount;
        if (DamageModifiersByEffectType.ContainsKey(effectType)) {
            DamageModifiersByEffectType[effectType] += amount;
        } else {
            DamageModifiersByEffectType[effectType] = amount;
        }
    }
    
    public DamageModifiers GetDamageModifiers() {
        OrderByEffectType();
        return this;
    }
    
    private void OrderByEffectType() {
        var orderedDamageModifiers = DamageModifiersByEffectType.OrderBy(x => x.Key);
        DamageModifiersByEffectType = GetDictFromOrderedDamageModifiers(orderedDamageModifiers);
    }
    
    private Dictionary<EffectType, double> GetDictFromOrderedDamageModifiers(IOrderedEnumerable<KeyValuePair<EffectType, double>> orderedDamageModifiers) {
        var orderedDamagedModifiersAsDict = new Dictionary<EffectType, double>();
        foreach (var damageModifier in orderedDamageModifiers) {
            orderedDamagedModifiersAsDict[damageModifier.Key] = damageModifier.Value;
        }
        return orderedDamagedModifiersAsDict;
    }
}