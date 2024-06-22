using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.DamageModifiersFiles;

public class DamageModification {
    public EffectType EffectType { get; set; }
    public double Amount { get; set; }
    
    public DamageModification(EffectType effectType, double amount) {
        EffectType = effectType;
        Amount = amount;
    }

    public DamageModification() {
        EffectType = EffectType.RegularDamageIncrease;
        Amount = 0;
    }
}