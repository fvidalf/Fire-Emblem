using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSkills.WeaponSkills;

public abstract class WeaponSkill: ConditionalSkill {

    protected string Weapon;

    public WeaponSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify, string weapon)
        : base(name, statsToModify) {
        Weapon = weapon;
    }

    protected override bool IsConditionMet() {
        var isThisWeaponUser = Character.Weapon == Weapon;
        return isThisWeaponUser;
    }
}