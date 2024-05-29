using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.StarterBonusSkills;

public class DeadlyBladeSkill: StarterSkill {

    public DeadlyBladeSkill()
        : base(
            "Deadly Blade",
            new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularBonus, new List<StatEffect> {
                    new StatEffect(Stat.Atk, 8),
                    new StatEffect(Stat.Spd, 8)
                }}
            }) {}

    protected override bool IsSpecificConditionMet() {
        return Character.Weapon == "Sword";
    }
}