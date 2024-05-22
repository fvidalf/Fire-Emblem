using Fire_Emblem.Skills.SingleCharacterSkills;

namespace Fire_Emblem.Skills.MultiCharacterSkills.DragonskinSkillFiles;

public class DragonskinSkill: MultiCharacterSkill {
    
    public DragonskinSkill()
        : base("Dragonskin", new SingleCharacterSkill[] {
            new DragonskinBonus(),
            new DragonskinNeutralize()
        }) {
    }
}