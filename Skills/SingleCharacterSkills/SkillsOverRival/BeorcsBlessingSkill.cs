using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.SkillsOverRival;

public class BeorcsBlessingSkill: BonusNeutralizer {

    public BeorcsBlessingSkill()
        : base("Beorc's Blessing",
            new List<Stat> {Stat.AtkBonus, Stat.SpdBonus, Stat.DefBonus, Stat.ResBonus}) {
    }
}