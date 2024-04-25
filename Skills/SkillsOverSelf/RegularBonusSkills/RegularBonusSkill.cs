using System.Reflection;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularBonusSkills;

// Regular Bonus: A bonus skill that is applied at the start of the turn and lasts indefinitely.
public abstract class RegularBonusSkill: SkillOverSelf {
    
    protected Dictionary<Stat, int> StatsToModify;
    
    protected RegularBonusSkill(string name, Dictionary<Stat, int> statsToModify)
        : base(name) {
        StatsToModify = statsToModify;
    }
    
    public override void Apply(GameStatus gameStatus) {
        base.Apply(gameStatus);
        ConcreteApply(gameStatus);
        IsActivated = true;
    }
    
    protected abstract void ConcreteApply(GameStatus gameStatus);
    
    protected override void SetEffectType() {
        SkillEffect.EffectType = EffectType.RegularBonus;
    }
}