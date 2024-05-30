using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;

public class BushidoRegularDamageIncreaseSkill: DamageModifierSkill {
    
    public BushidoRegularDamageIncreaseSkill()
        : base("Bushido Regular Damage Increase",
            new DamageModification(EffectType.RegularDamageIncrease, 7)
        ) {
    }
}