using Fire_Emblem.CharacterFiles.StatFiles;
namespace Fire_Emblem.Skills.MultiSkills.PushSkills;

public class DefResPushSkill : PushSkill {
    
    public DefResPushSkill()
        : base(Stat.Def, Stat.Res) {
    }
}