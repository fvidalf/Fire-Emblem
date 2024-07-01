using Fire_Emblem.Skills.SingleSkills.CombatHealingSkill;
using Fire_Emblem.Skills.SingleSkills.PreCombatHealingSkills;
namespace Fire_Emblem.Skills.MultiSkills.CombatHealingMultiSkills;

public class SolarBraceSkill : CombatHealingMultiSkill {
    
    public SolarBraceSkill()
        : base("Solar Brace", new SolarBracePreCombatHealingSkill(), new SolarBraceCombatHealingSkill()) {}
}