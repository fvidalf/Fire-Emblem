using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class MoonTwinWingRegularDamageReductionSkill: ConditionalDamageModifierSkill {
    
    public MoonTwinWingRegularDamageReductionSkill() : base("Moon-Twin Wing: Regular Damage Reduction",
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
        DamageModification.SetAmount(damageIncrease);
    }
    
    protected override bool IsConditionMet() {
        var unit = RoundStatus.ActivatingCharacterModel;
        var isUnitAtOrAboveQuarterHp = unit.Hp >= unit.BaseHp / 4;
        var isUnitSpdHigherThanRival = unit.Spd > RoundStatus.RivalCharacterModel.Spd;
        return isUnitAtOrAboveQuarterHp && isUnitSpdHigherThanRival;
    }
}