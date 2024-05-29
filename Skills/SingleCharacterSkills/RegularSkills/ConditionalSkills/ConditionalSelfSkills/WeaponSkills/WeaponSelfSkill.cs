using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills.WeaponSkills;

public abstract class WeaponSelfSkill: ConditionalSelfSkill {

    protected string Weapon;

    public WeaponSelfSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify, string weapon)
        : base(name, statsToModify) {
        Weapon = weapon;
    }

    protected override bool IsConditionMet() {
        var isThisWeaponUser = Character.Weapon == Weapon;
        return isThisWeaponUser;
    }
}