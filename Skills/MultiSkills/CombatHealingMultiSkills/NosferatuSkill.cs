using Fire_Emblem.Skills.SingleSkills.CombatHealingSkill;
using Fire_Emblem.Skills.SingleSkills.PreCombatHealingSkills;
namespace Fire_Emblem.Skills.MultiSkills.CombatHealingMultiSkills;

public class NosferatuSkill : CombatHealingMultiSkill {
    
    public NosferatuSkill()
        : base("Nosferatu", new NosferatuPreCombatHealingSkill(), new NosferatuCombatHealingSkill()) {}
}