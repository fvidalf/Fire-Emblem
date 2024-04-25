using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillEffectFiles;

public class StatEffect {
    public Stat Stat { get; set; }
    public int Amount { get; set; }
    
    public StatEffect(Stat stat, int amount) {
        Stat = stat;
        Amount = amount;
    }
}