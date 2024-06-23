using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class DivineRecreationNextAttackDamageIncreaseSkill: ConditionalDamageModifierSkill {
    public DivineRecreationNextAttackDamageIncreaseSkill()
        : base("Divine Recreation: Next Attack Damage ", new DamageModification()
        ) {}
    
    public override void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        DetermineTarget();
        if (IsConditionMet()) {
            DetermineDamageModification();
            Character.UpdateDamageModifiers(DamageModification);
        }
    }
    
    protected override bool IsConditionMet() {
        var isRivalAtOrAboveHalfHp = RoundStatus.RivalCharacterModel.Hp >= RoundStatus.RivalCharacterModel.BaseHp / 2;
        return isRivalAtOrAboveHalfHp;
    }

    private void DetermineDamageModification() {
        var attacker = RoundStatus.RivalCharacterModel;
        var target = RoundStatus.ActivatingCharacterModel;
        
        var isRivalFirstToAttack = RoundStatus.RivalCharacterModel == RoundStatus.FirstCharacterModel;
        var originalDamage = DamageCalculator.CalculateDamageWithOnlyIncreases(attacker, target, RoundStatus.RoundPhase);
        var finalDamage = DamageCalculator.CalculateFinalDamage(attacker, target, RoundStatus.RoundPhase);
        
        if (isRivalFirstToAttack) {
            DamageModification.EffectType = EffectType.FirstAttackDamageIncrease;
        } else {
            DamageModification.EffectType = EffectType.FollowUpDamageIncrease;
            
        }
        var rivalDamageDiff = originalDamage - finalDamage;
        
        DamageModification.Amount = rivalDamageDiff;
    }
}