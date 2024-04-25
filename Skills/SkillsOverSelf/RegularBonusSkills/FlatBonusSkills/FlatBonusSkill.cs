using System.Reflection;
using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills.FlatBonusSkills;

// Flat Bonus: A regular bonus skill with no conditions for being applied. 
public abstract class FlatBonusSkill: RegularBonusSkill {
    
    protected FlatBonusSkill(string name, Dictionary<Stat, int> statsToModify)
    : base(name, statsToModify) {}
    
    protected override void ConcreteApply(GameStatus gameStatus) {
        foreach (KeyValuePair<Stat, int> stat in StatsToModify) {
            UpdateCharacterStat(Character, stat);
        }
        SetModifiedStats();
    }
}