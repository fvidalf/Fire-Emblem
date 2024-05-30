using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills.FlatDamageModifierSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills;

public class BushidoSkill: MultiCharacterSkill {
    
    public BushidoSkill()
        : base("Bushido", new ISingleCharacterSkill[] {
            new BushidoFlatSkill(),
            new DodgeSkill()
        }) {
    }
}