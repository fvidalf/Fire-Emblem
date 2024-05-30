namespace Fire_Emblem.Skills.SkillEffectFiles;

public class DamageModifiers {
    public Dictionary<EffectType, int> DamageModifiersByEffectType { get; }
    
    public DamageModifiers() {
        DamageModifiersByEffectType = new Dictionary<EffectType, int>();
    }
    
    public void UpdateDamageModifier(EffectType effectType, int amount) {
        if (DamageModifiersByEffectType.ContainsKey(effectType)) {
            DamageModifiersByEffectType[effectType] += amount;
        } else {
            DamageModifiersByEffectType[effectType] = amount;
        }
    }
}