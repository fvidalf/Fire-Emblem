using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.FlatBonusSkills;

public class ResistanceSkill: FlatBonusSkill {
    public ResistanceSkill()
        : base("Resistance +5", new Dictionary<Stat, int> {{Stat.Res, 5}}) {}
}