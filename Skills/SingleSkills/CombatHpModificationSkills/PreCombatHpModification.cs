using Fire_Emblem.CharacterFiles;
using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;
namespace Fire_Emblem.Skills.SingleSkills.CombatHpModificationSkills;


public class PreCombatHpModificationSkill : ITimedSkill, IHasSkillEffect {
    
    public string Name { get; set; }
    private int HpModification { get; }
    protected CharacterModel Character;
    private SkillEffect SkillEffect { get; set; }
    protected RoundStatus RoundStatus;
    public TimeToApply TimeToApply { get; } = TimeToApply.PreCombat;
    
    public PreCombatHpModificationSkill(string name, int hpModification) {
        SkillEffect = new SkillEffect();
        Name = name;
        HpModification = hpModification;
    }
    
    public void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        Character = RoundStatus.ActivatingCharacterModel;
        if (!IsConditionMet() || Character.IsDead) {
            UpdateSkillEffect(EffectType.HpModificationPreCombat, new StatEffect(Stat.PreCombatHpModification, 0));
            return;
        }
        Character.ApplyPreCombatHpModification(HpModification);
        UpdateSkillEffect(EffectType.HpModificationPreCombat, new StatEffect(Stat.PreCombatHpModification, HpModification));
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