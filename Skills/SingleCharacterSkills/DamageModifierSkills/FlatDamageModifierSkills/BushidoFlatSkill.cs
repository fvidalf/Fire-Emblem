using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills.FlatDamageModifierSkills;

public class BushidoFlatSkill: DamageModifierSkill {
    
    public BushidoFlatSkill()
        : base("BushidoFlat",
            new DamageModification(EffectType.RegularDamageIncrease, 7)
        ) {
    }
}