using System.Collections;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.DamageModifiersFiles;

public class DamageModification {
    public EffectType EffectType { get; }
    public double Amount { get; private set; }
    
    public DamageModification(EffectType effectType, double amount) {
        EffectType = effectType;
        Amount = amount;
    }
    
    public void SetAmount(double amount) {
        Amount = amount;
    }
}