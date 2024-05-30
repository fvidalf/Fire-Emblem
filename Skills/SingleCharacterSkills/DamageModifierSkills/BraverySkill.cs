using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills;

public class BraverySkill: DamageModifierSkill, ITargetedSkill {
    
    public BraverySkill() : base("Bravery") {}
    
    public override void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        DetermineTarget();
        Character.UpdateDamageModifiers(EffectType.RegularDamageIncrease, 5);
    }
    
    public void DetermineTarget() {
        Character = RoundStatus.ActivatingCharacterModel;
    }
}