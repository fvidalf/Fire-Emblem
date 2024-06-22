using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;

public class ExtraChivalryRegularDamagePercentageReductionSkill: DamageModifierSkill {
    
    public ExtraChivalryRegularDamagePercentageReductionSkill()
        : base("Extra Chivalry: Regular Damage Percentage Reduction",
            new DamageModification(EffectType.RegularDamagePercentageReduction, 0)
        ) {
    }
    
    public override void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        DetermineTarget();
        SetDamageReduction();
        Character.UpdateDamageModifiers(DamageModification);
    }
    
    private void SetDamageReduction() {
        var rivalHpAsPercentage = ((double) RoundStatus.RivalCharacterModel.Hp / RoundStatus.RivalCharacterModel.BaseHp) * 100;
        Console.WriteLine($"Rival hp as percentage: {rivalHpAsPercentage}");
        var damageReduction = Math.Floor(rivalHpAsPercentage / 2) / 100;
        Console.WriteLine($"Damage reduction: {damageReduction}");
        DamageModification.Amount = damageReduction;
    }
}