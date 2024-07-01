using Fire_Emblem.Skills.MultiSkills.CombatHealingMultiSkills;
using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.CombatHealingSkill;
using Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;
using Fire_Emblem.Skills.SingleSkills.PreCombatHealingSkills;
namespace Fire_Emblem.Skills.MultiSkills;

public class EclipseBraceSkill : MultiSkill {
    
    public EclipseBraceSkill()
        : base(
            "Eclipse Brace", new ISingleSkill[] {
                new EclipseBraceRegularDamageIncreaseSkill(), 
                new SolarBracePreCombatHealingSkill(),
                new SolarBraceCombatHealingSkill()
            }) {}
}