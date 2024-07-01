using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;
namespace Fire_Emblem.Skills.SingleSkills.CombatHealingSkill;

public class CombatHealingSkill : ITimedSkill, IHasSkillEffect {
    
    public string Name { get; set; }
    private double HealingPercentage { get; }
    protected CharacterModel Character;
    private SkillEffect SkillEffect { get; set; }
    protected RoundStatus RoundStatus;
    public TimeToApply TimeToApply { get; } = TimeToApply.Combat;
    
    public CombatHealingSkill(string name, double healingPercentage) {
        Name = name;
        HealingPercentage = healingPercentage;
    }
    
    public void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        Character = RoundStatus.ActivatingCharacterModel;
        var hasJustAttacked = Character.HasJustAttacked;
        if (!hasJustAttacked || !IsConditionMet()) {
            UpdateSkillEffect(EffectType.HpModificationInCombat, new StatEffect(Stat.HpBonusByPercentage, 0));
            return;
        }
        var hpModification = (int) (Character.LastDamageDealt * HealingPercentage);
        Character.Hp = int.Min(Character.BaseHp, Character.Hp + hpModification);
        Character.HasJustAttacked = false;
        UpdateSkillEffect(EffectType.HpModificationInCombat, new StatEffect(Stat.HpBonusByPercentage, hpModification));
    }
    
    public Dictionary<CharacterModel, SkillEffect> GetSkillEffect() {
        return new Dictionary<CharacterModel, SkillEffect> { {Character, SkillEffect} };
    }
    
    public virtual void Reset() {
        SkillEffect = new SkillEffect();
    }
    
    private void UpdateSkillEffect(EffectType effectType, StatEffect statEffect) {
        var newStatEffect = new StatEffect(statEffect.Stat, statEffect.Amount);
        var statEffects = new List<StatEffect> {newStatEffect};
        SkillEffect.StatEffectsByEffectType[effectType] = statEffects;
    }

    protected virtual bool IsConditionMet() {
        return true;
    }
    
}