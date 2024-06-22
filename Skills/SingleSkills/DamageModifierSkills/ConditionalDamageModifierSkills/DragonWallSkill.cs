using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class DragonWallSkill: ConditionalDamageModifierSkill {
    
    public DragonWallSkill() : base("Dragon Wall",
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
        var resDiff = RoundStatus.ActivatingCharacterModel.Res - RoundStatus.RivalCharacterModel.Res;
        var resDiffAsPercentage = (double) resDiff / 100;
        var damageIncrease = Math.Min(resDiffAsPercentage * 4, 0.4);
        Console.WriteLine($"resDiff: {resDiff}");
        Console.WriteLine($"resDiffAsPercentage: {resDiffAsPercentage}");
        Console.WriteLine($"Damage increase: {damageIncrease}");
        DamageModification.Amount = damageIncrease;
    }
    
    protected override bool IsConditionMet() {
        var isResHigherThanRival = RoundStatus.ActivatingCharacterModel.Res > RoundStatus.RivalCharacterModel.Res;
        return isResHigherThanRival;
    }
    
}