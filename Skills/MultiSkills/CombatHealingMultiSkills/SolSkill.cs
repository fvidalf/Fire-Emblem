using Fire_Emblem.Skills.SingleSkills.CombatHealingSkill;
using Fire_Emblem.Skills.SingleSkills.PreCombatHealingSkills;

namespace Fire_Emblem.Skills.MultiSkills.CombatHealingMultiSkills;

public class SolSkill : CombatHealingMultiSkill {
    
    public SolSkill()
        : base("Sol", new SolPreCombatHealingSkill(), new SolCombatHealingSkill()) {}
}