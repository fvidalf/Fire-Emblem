using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;
namespace Fire_Emblem.Skills.SingleSkills.PreCombatHealingSkills;

public abstract class PreCombatHealingSkill : ITimedSkill, IHasSkillEffect {
    public string Name { get; set; }
    private double HealingPercentage { get; set; }
    protected CharacterModel? Character;
    private SkillEffect SkillEffect { get; set; }
    protected RoundStatus RoundStatus;
    public TimeToApply TimeToApply { get; set; } = TimeToApply.PreCombat;
    
    public PreCombatHealingSkill(string name, double healingPercentage) {
        Name = name;
        SkillEffect = new SkillEffect();
        HealingPercentage = healingPercentage;
    }
    
    public virtual void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        Character = RoundStatus.ActivatingCharacterModel;
        if (!IsConditionMet()) {
            return;
        }
        Character.HpBonusByPercentage += HealingPercentage;
        UpdateSkillEffect(EffectType.HpBonusByPercentage, new StatEffect(Stat.HpBonusByPercentage, (int) (HealingPercentage * 100)));
    }
    
    public Dictionary<CharacterModel, SkillEffect> GetSkillEffect() {
        return new Dictionary<CharacterModel, SkillEffect> { {Character, SkillEffect} };
    }
    
    public virtual void Reset() {
        SkillEffect = new SkillEffect();
    }
    
    private void UpdateSkillEffect(EffectType effectType, StatEffect statEffect) {
        if (SkillEffect.StatEffectsByEffectType.ContainsKey(effectType)) {
            var statEffects = SkillEffect.StatEffectsByEffectType[effectType];
            var newStatEffect = new StatEffect(statEffect.Stat, statEffect.Amount);
            statEffects.Add(newStatEffect);
        }
        else {
            var newStatEffect = new StatEffect(statEffect.Stat, statEffect.Amount);
            var statEffects = new List<StatEffect> {newStatEffect};
            SkillEffect.StatEffectsByEffectType[effectType] = statEffects;
        }
    }

    protected virtual bool IsConditionMet() {
        return true;
    }
}