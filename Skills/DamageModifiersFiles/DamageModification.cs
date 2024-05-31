using System.Collections;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.DamageModifiersFiles;

public class DamageModification {
    public EffectType EffectType { get; private set; }
    public double Amount { get; private set; }
    
    public DamageModification(EffectType effectType, double amount) {
        EffectType = effectType;
        Amount = amount;
    }

    public DamageModification() {
        EffectType = EffectType.RegularDamageIncrease;
        Amount = 0;
    }
    
    public void SetAmount(double amount) {
        Amount = amount;
    }
    
    public void SetEffectType(EffectType effectType) {
        EffectType = effectType;
    }
}