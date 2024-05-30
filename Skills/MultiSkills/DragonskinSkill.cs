using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SingleSkills.Neutralizers;
using Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public class DragonskinSkill: MultiSkill {
    
    public DragonskinSkill()
        : base("Dragonskin", new StatModifierSkill[] {
            new DragonskinBonus(),
            new DragonskinNeutralize()
        }) {
    }
}