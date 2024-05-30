using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills;

public abstract class DamageModifierSkill: ISingleCharacterSkill, ITargetedSkill{
    
    public string Name { get; set; }
    public CharacterModel? Character;
    protected RoundStatus RoundStatus;
    protected DamageModification DamageModification;
    
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