using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillEffectFiles;

public class SkillNeutralize {
    List<Stat> StatsToNeutralize { get; set; }
    
    public SkillNeutralize(List<Stat> statsToNeutralize) {
        StatsToNeutralize = statsToNeutralize;
    }
}