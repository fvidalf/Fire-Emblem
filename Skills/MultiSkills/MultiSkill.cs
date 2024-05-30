using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SingleSkills;

namespace Fire_Emblem.Skills.MultiSkills;

public abstract class MultiSkill: IBaseSkill {
    
    public string Name { get; set; }
    public bool IsActivated { get; set; }
    protected ISingleSkill[] Skills;
    protected CharacterModel? Character;
    protected RoundStatus RoundStatus;
    
    protected MultiSkill(string name, ISingleSkill[] skills) {
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

    public List<ISingleSkill> DecomposeIntoList() {
        return Skills.ToList();
    }
}