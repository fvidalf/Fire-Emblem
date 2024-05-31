using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class DragonsWrathFirstAttackDamageIncreaseSkill: ConditionalDamageModifierSkill {
    
    public DragonsWrathFirstAttackDamageIncreaseSkill()
        : base("Dragon's Wrath: First Attack Damage Increase", 
            new DamageModification(EffectType.FirstAttackDamageIncrease, 0)
        ) { }
    
    public override void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        DetermineTarget();
        SetDamageIncrease();
        if (IsConditionMet()) {
            Character.UpdateDamageModifiers(DamageModification);
        }
        
    }
    
    private void SetDamageIncrease() {
        var damageIncrease = (RoundStatus.ActivatingCharacterModel.Atk - RoundStatus.RivalCharacterModel.Res) * 0.25;
        damageIncrease = Math.Floor(damageIncrease);
        DamageModification.SetAmount(damageIncrease);
    }
    
    protected override bool IsConditionMet() {
        var isUnitAtkHigherThanRivalRes = RoundStatus.ActivatingCharacterModel.Atk > RoundStatus.RivalCharacterModel.Res;
        return isUnitAtkHigherThanRivalRes;
    }
    
}