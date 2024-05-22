using System.Reflection;
using System.Reflection.Metadata;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.MultiCharacterSkills;

public abstract class MultiCharacterSkill: IMultiCharacterSkill {
    
    public string Name { get; set; }
    public bool IsActivated { get; set; }
    protected SingleCharacterSkill[] Skills;
    protected Character? Character;
    protected GameStatus GameStatus;
    
    protected MultiCharacterSkill(string name, SingleCharacterSkill[] skills) {
        Name = name;
        IsActivated = false; 
        Skills = skills;
    }
    
    public virtual void Apply(GameStatus gameStatus) {
        GameStatus = gameStatus;
        Console.WriteLine($"Applying {Name} by {GameStatus.ActivatingCharacter.Name}");
        foreach (var skill in Skills) {
            skill.Apply(gameStatus);
        }
    }
    
    public Dictionary<Character, SkillEffect> GetModifiedStats() {
        var modifiedStats = new Dictionary<Character, SkillEffect>();
        foreach (var skill in Skills) {
            modifiedStats = JoinByCharacter(modifiedStats, skill.GetModifiedStats());
        }
        return modifiedStats;
    }
    
    private Dictionary<Character, SkillEffect> JoinByCharacter( Dictionary<Character, SkillEffect> modifiedStats, Dictionary<Character, SkillEffect> newStats) {
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