using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.CombatHealingSkill;
using Fire_Emblem.Skills.SingleSkills.PreCombatHealingSkills;
namespace Fire_Emblem.Skills.MultiSkills.CombatHealingMultiSkills;

public abstract class CombatHealingMultiSkill: MultiSkill {
    
    protected CombatHealingMultiSkill(string name, PreCombatHealingSkill preCombatHealingSkill, CombatHealingSkill combatHealingSkill)
        : base(name, new ISingleSkill[] {
                preCombatHealingSkill,
                combatHealingSkill
            }
        ) {}
    
}