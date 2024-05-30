using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class DodgeSkill: ConditionalDamageModifierSkill {
    
    public DodgeSkill() : base("Dodge",
        new DamageModification(EffectType.RegularDamagePercentageReduction, 0)
    ) {}

    public override void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        DetermineTarget();
        SetDamageIncrease();
        if (IsConditionMet()) {
            Character.UpdateDamageModifiers(DamageModification);
        }
        
    }
    
    private void SetDamageIncrease() {
        var spdDiff = RoundStatus.ActivatingCharacterModel.Spd - RoundStatus.RivalCharacterModel.Spd;
        var spdDiffAsPercentage = (double) spdDiff / 100;
        var damageIncrease = Math.Min(spdDiffAsPercentage * 4, 0.4);
        Console.WriteLine($"spdDiff: {spdDiff}");
        Console.WriteLine($"spdDiffAsPercentage: {spdDiffAsPercentage}");
        Console.WriteLine($"Damage increase: {damageIncrease}");
        DamageModification.SetAmount(damageIncrease);
    }
    
    protected override bool IsConditionMet() {
        var isSpdHigherThanRival = RoundStatus.ActivatingCharacterModel.Spd > RoundStatus.RivalCharacterModel.Spd;
        return isSpdHigherThanRival;
    }
    
}