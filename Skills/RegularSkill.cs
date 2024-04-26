using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills;

// Regular Bonus: A bonus skill that is applied at the start of the turn and lasts indefinitely.
public abstract class RegularSkill: BaseSkill {
    
    protected Dictionary<EffectType, List<StatEffect>> StatsToModify;
    
    protected RegularSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name) {
        StatsToModify = statsToModify;
    }
    
    public override void Apply(GameStatus gameStatus) {
        base.Apply(gameStatus);
        ConcreteApply(gameStatus);
        IsActivated = true;
    }
    
    protected abstract void ConcreteApply(GameStatus gameStatus);
}