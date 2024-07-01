using Fire_Emblem.CharacterFiles.StatFiles;
namespace Fire_Emblem.Skills.MultiSkills.PushSkills;

public class AtkResPushSkill : PushSkill {
    
    public AtkResPushSkill()
        : base(Stat.Atk, Stat.Res) {
    }
}