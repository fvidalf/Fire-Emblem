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
        DamageModifiersByEffectType[effectType] += amount;
    }
    
    public DamageModifiers GetDamageModifiers() {
        return this;
    }
    
    public double GetRegularDamageIncrease() {
        return DamageModifiersByEffectType[EffectType.RegularDamageIncrease];
    }
    
    public double GetFirstAttackDamageIncrease() {
        return DamageModifiersByEffectType[EffectType.FirstAttackDamageIncrease];
    }
    
    public double GetFollowUpDamageIncrease() {
        return DamageModifiersByEffectType[EffectType.FollowUpDamageIncrease];
    }
    
    public double GetRegularDamagePercentageReduction() {
        return DamageModifiersByEffectType[EffectType.RegularDamagePercentageReduction];
    }
    
    public double GetFirstAttackDamagePercentageReduction() {
        return DamageModifiersByEffectType[EffectType.FirstAttackDamagePercentageReduction];
    }
    
    public double GetFollowUpDamagePercentageReduction() {
        return DamageModifiersByEffectType[EffectType.FollowUpDamagePercentageReduction];
    }
    
    public double GetRegularDamageAbsoluteReduction() {
        return DamageModifiersByEffectType[EffectType.RegularDamageAbsoluteReduction];
    }
    
    public void ResetRegularDamageIncrease() {
        DamageModifiersByEffectType[EffectType.RegularDamageIncrease] = 0;
    }
    
    public void ResetFirstAttackDamageIncrease() {
        DamageModifiersByEffectType[EffectType.FirstAttackDamageIncrease] = 0;
    }
    
    public void ResetFollowUpDamageIncrease() {
        DamageModifiersByEffectType[EffectType.FollowUpDamageIncrease] = 0;
    }
    
    public void ResetRegularDamagePercentageReduction() {
        DamageModifiersByEffectType[EffectType.RegularDamagePercentageReduction] = 0;
    }
    
    public void ResetFirstAttackDamagePercentageReduction() {
        DamageModifiersByEffectType[EffectType.FirstAttackDamagePercentageReduction] = 0;
    }
    
    public void ResetFollowUpDamagePercentageReduction() {
        DamageModifiersByEffectType[EffectType.FollowUpDamagePercentageReduction] = 0;
    }
    
    public void ResetRegularDamageAbsoluteReduction() {
        DamageModifiersByEffectType[EffectType.RegularDamageAbsoluteReduction] = 0;
    }
}