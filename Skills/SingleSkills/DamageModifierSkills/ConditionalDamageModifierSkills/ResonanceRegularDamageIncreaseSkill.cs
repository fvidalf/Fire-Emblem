using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;
namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class ResonanceRegularDamageIncreaseSkill : ConditionalDamageModifierSkill {
    
    public ResonanceRegularDamageIncreaseSkill(): base("Eclipse Brace Regular Damage Increase",
        new DamageModification(EffectType.RegularDamageIncrease, 3)
    ) {}
    
    protected override bool IsConditionMet() {
        var isCharacterMagicUser = Character.Weapon == "Magic";
        var isHpGreaterOrAt2 = Character.Hp >= 2;
        return isCharacterMagicUser && isHpGreaterOrAt2;
    }
}