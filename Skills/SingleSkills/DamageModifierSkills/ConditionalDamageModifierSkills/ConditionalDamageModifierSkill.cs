using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public abstract class ConditionalDamageModifierSkill: DamageModifierSkill {
    
    protected ConditionalDamageModifierSkill(string name, DamageModification damageModification) : 
        base(name, damageModification) {}

    public override void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        DetermineTarget();
        if (IsConditionMet()) {
            Character.UpdateDamageModifiers(DamageModification);
        }
    }
    
    protected abstract bool IsConditionMet();
}