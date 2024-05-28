using Fire_Emblem.CharacterFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.MultiCharacterSkills;

public abstract class MultiCharacterSkill: IMultiCharacterSkill {
    
    public string Name { get; set; }
    public bool IsActivated { get; set; }
    protected SingleCharacterSkill[] Skills;
    protected CharacterModel? Character;
    protected RoundStatus RoundStatus;
    
    protected MultiCharacterSkill(string name, SingleCharacterSkill[] skills) {
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
    
    public Dictionary<CharacterModel, SkillEffect> GetModifiedStats() {
        var modifiedStats = new Dictionary<CharacterModel, SkillEffect>();
        foreach (var skill in Skills) {
            modifiedStats = JoinByCharacter(modifiedStats, skill.GetModifiedStats());
        }
        return modifiedStats;
    }
    
    private Dictionary<CharacterModel, SkillEffect> JoinByCharacter( Dictionary<CharacterModel, SkillEffect> modifiedStats, Dictionary<CharacterModel, SkillEffect> newStats) {
        foreach (var (oldCharacter, oldSkillEffect) in modifiedStats) {
            foreach (var (newCharacter, newSkillEffect) in newStats) {
                if (oldCharacter == newCharacter) {
                    oldSkillEffect.Join(newSkillEffect);
                }
            }
        }
        return modifiedStats;
    }
    
    public virtual void Reset() {
        IsActivated = false;
    }

    public List<SingleCharacterSkill> DecomposeIntoList() {
        return Skills.ToList();
    }
}