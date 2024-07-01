using Fire_Emblem.CharacterFiles.StatFiles;
namespace Fire_Emblem.Skills.MultiSkills.PushSkills;

public class AtkSpdPushSkill : PushSkill {
    
    public AtkSpdPushSkill()
        : base(Stat.Atk, Stat.Spd) {
    }
}