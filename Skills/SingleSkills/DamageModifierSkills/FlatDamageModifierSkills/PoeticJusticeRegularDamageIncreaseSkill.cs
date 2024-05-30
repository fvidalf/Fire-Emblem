using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;

public class PoeticJusticeRegularDamageIncreaseSkill: DamageModifierSkill {
    
    public PoeticJusticeRegularDamageIncreaseSkill() : base("Poetic Justice Regular Damage Increase",
        new DamageModification(EffectType.RegularDamageIncrease, 0)
    ) {}
    
    public override void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        DetermineTarget();
        SetDamageIncrease();
        Character.UpdateDamageModifiers(DamageModification);
    }
    
    private void SetDamageIncrease() {
        var rivalAtk = RoundStatus.RivalCharacterModel.Atk;
        var damageIncrease = Convert.ToInt32(Math.Floor(rivalAtk * 0.15));
        DamageModification.SetAmount(damageIncrease);
    }
}