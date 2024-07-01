using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills;

public abstract class DamageModifierSkill: ITimedSkill, ITargetedSkill {
    
    public string Name { get; set; }
    public CharacterModel? Character;
    protected RoundStatus RoundStatus;
    protected DamageModification DamageModification;
    public TimeToApply TimeToApply { get; set; } = TimeToApply.PreCombat;
    
    protected DamageModifierSkill(string name, DamageModification damageModification) {
        Name = name;
        DamageModification = damageModification;
    }

    public virtual void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        DetermineTarget();
        Character.UpdateDamageModifiers(DamageModification);
    }
    
    public void DetermineTarget() {
        Character = RoundStatus.ActivatingCharacterModel;
    }
    
    public void Reset() {
    }
}