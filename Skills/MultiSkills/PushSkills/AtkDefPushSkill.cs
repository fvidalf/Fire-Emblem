using Fire_Emblem.CharacterFiles.StatFiles;
namespace Fire_Emblem.Skills.MultiSkills.PushSkills;

public class AtkDefPushSkill : PushSkill {
    
    public AtkDefPushSkill()
        : base(Stat.Atk, Stat.Def) {
    }
}