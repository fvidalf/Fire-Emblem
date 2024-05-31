using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.FlatDamageModifierSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class DragonsWrathSkill: MultiSkill {
    public DragonsWrathSkill()
        : base("Dragon's Wrath", new ISingleSkill[] {
            
            new DragonsWrathFirstAttackDamageReductionSkill(),
            new DragonsWrathFirstAttackDamageIncreaseSkill(),
            
        }) {}
}