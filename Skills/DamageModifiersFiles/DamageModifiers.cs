using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.DamageModifiersFiles;

public class DamageModifiers {
    public Dictionary<EffectType, double> DamageModifiersByEffectType { get; set; }
    
    public DamageModifiers() {
        DamageModifiersByEffectType = new Dictionary<EffectType, double>
        {
            {EffectType.RegularDamageIncrease, 0},
            {EffectType.FirstAttackDamageIncrease, 0},
            {EffectType.FollowUpDamageIncrease, 0},
            {EffectType.RegularDamagePercentageReduction, 0},
            {EffectType.FirstAttackDamagePercentageReduction, 0},
            {EffectType.FollowUpDamagePercentageReduction, 0},
            {EffectType.RegularDamageAbsoluteReduction, 0}
        };
    }
    
    public void UpdateDamageModifier(DamageModification damageModification) {
        var effectType = damageModification.EffectType;
        var amount = damageModification.Amount;

        switch (effectType) {
            case EffectType.RegularDamageIncrease:
            case EffectType.FirstAttackDamageIncrease:
            case EffectType.FollowUpDamageIncrease:
            case EffectType.RegularDamageAbsoluteReduction:
                IncreaseAbsoluteDamageModifier(effectType, amount);
                break;
            case EffectType.RegularDamagePercentageReduction:
            case EffectType.FirstAttackDamagePercentageReduction:
            case EffectType.FollowUpDamagePercentageReduction:
                ReducePercentageDamageModifier(effectType, amount);
                break;
        }
    }
    
    private void IncreaseAbsoluteDamageModifier(EffectType effectType, double amount) {
        DamageModifiersByEffectType[effectType] += amount;
    }
    
    private void ReducePercentageDamageModifier(EffectType effectType, double amount) {
        DamageModifiersByEffectType[effectType] = 1 - (1 - DamageModifiersByEffectType[effectType]) * (1 - amount);
    }
    
    public DamageModifiers GetDamageModifiers() {
        return this;
    }
    
    public double GetDamageModifier(EffectType effectType) {
        var damageModifier = DamageModifiersByEffectType[effectType];
        return damageModifier;
    }

    public void ResetDamageModifier(EffectType effectType) {
        DamageModifiersByEffectType[effectType] = 0;
    }
}