using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillEffectFiles;

public class SkillEffect
{
    public EffectType EffectType { get; set; }
    public Dictionary<Stat, int>? Stats { get; set; }
}