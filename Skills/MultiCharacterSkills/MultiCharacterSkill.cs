using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.MultiCharacterSkills;

public abstract class MultiCharacterSkill: IBaseSkill {
    
    public string Name { get; set; }
    public bool IsActivated { get; set; }
    protected ISingleCharacterSkill[] Skills;
    protected CharacterModel? Character;
    protected RoundStatus RoundStatus;
    
    protected MultiCharacterSkill(string name, StatModifierSkill[] skills) {
        Name = name;
        IsActivated = false; 
        Skills = skills;
    }
    
    public virtual void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        Console.WriteLine($"Applying {Name} by {RoundStatus.ActivatingCharacterModel.Name}");
        foreach (var skill in Skills) {
            skill.Apply(roundStatus);
        }
    }

    public List<ISingleCharacterSkill> DecomposeIntoList() {
        return Skills.ToList();
    }
}