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
        var regularDamageIncrease = DamageModifiersByEffectType[EffectType.RegularDamageIncrease];
        return regularDamageIncrease;
    }
    
    public double GetFirstAttackDamageIncrease() {
        var firstAttackDamageIncrease = DamageModifiersByEffectType[EffectType.FirstAttackDamageIncrease];
        return firstAttackDamageIncrease;
    }
    
    public double GetFollowUpDamageIncrease() {
        var followUpDamageIncrease = DamageModifiersByEffectType[EffectType.FollowUpDamageIncrease];
        return followUpDamageIncrease;
    }
    
    public double GetRegularDamagePercentageReduction() {
        var regularDamagePercentageReduction = DamageModifiersByEffectType[EffectType.RegularDamagePercentageReduction];
        return regularDamagePercentageReduction;
    }
    
    public double GetFirstAttackDamagePercentageReduction() {
        var firstAttackDamagePercentageReduction = DamageModifiersByEffectType[EffectType.FirstAttackDamagePercentageReduction];
        return firstAttackDamagePercentageReduction;
    }
    
    public double GetFollowUpDamagePercentageReduction() {
        var followUpDamagePercentageReduction = DamageModifiersByEffectType[EffectType.FollowUpDamagePercentageReduction];
        return followUpDamagePercentageReduction;
    }
    
    public double GetRegularDamageAbsoluteReduction() {
        var regularDamageAbsoluteReduction = DamageModifiersByEffectType[EffectType.RegularDamageAbsoluteReduction];
        return regularDamageAbsoluteReduction;
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