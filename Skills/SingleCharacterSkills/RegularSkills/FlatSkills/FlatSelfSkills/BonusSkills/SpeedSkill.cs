using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.FlatSkills.FlatSelfSkills.BonusSkills;

public class SpeedSkill: FlatSelfSkill{
    public SpeedSkill()
        : base("Speed +5", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                new StatEffect(Stat.Spd, 5)
            }}
        }) {}
}