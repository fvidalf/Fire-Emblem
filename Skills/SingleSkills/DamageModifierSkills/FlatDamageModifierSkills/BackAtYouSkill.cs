using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;

public class BackAtYouSkill: ConditionalDamageModifierSkill {
    
    public BackAtYouSkill()
        : base("Back at You",
            new DamageModification(EffectType.RegularDamageIncrease, 0)
        ) {
    }
    
    public override void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        DetermineTarget();
        SetDamageIncrease();
        if (IsConditionMet()) {
            Character.UpdateDamageModifiers(DamageModification);
        }
    }
    
    private void SetDamageIncrease() {
        var hpLost = RoundStatus.ActivatingCharacterModel.BaseHp - RoundStatus.ActivatingCharacterModel.Hp;
        Console.WriteLine($"Hp lost: {hpLost}");
        var damageIncrease = hpLost / 2;
        DamageModification.Amount = damageIncrease;
    }
    
    protected override bool IsConditionMet() {
        var isRivalFirstToAttack = RoundStatus.RivalCharacterModel == RoundStatus.FirstCharacterModel;
        return isRivalFirstToAttack;
    }
}