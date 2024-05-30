using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class BlueSkiesSkill: MultiSkill {

    public BlueSkiesSkill() : base("Blue Skies", new ISingleSkill[] {
        new BraverySkill(),
        new GentilitySkill()
    }) { }

}