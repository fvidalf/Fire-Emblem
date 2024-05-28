using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers;
using Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills;

public class DragonskinSkill: MultiCharacterSkill {
    
    public DragonskinSkill()
        : base("Dragonskin", new SingleCharacterSkill[] {
            new DragonskinBonus(),
            new DragonskinNeutralize()
        }) {
    }
}