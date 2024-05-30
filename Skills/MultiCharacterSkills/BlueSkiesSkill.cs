using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills.FlatDamageModifierSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills;

public class BlueSkiesSkill: MultiCharacterSkill {

    public BlueSkiesSkill() : base("Blue Skies", new ISingleCharacterSkill[] {
        new BraverySkill(),
        new GentilitySkill()
    }) { }

}