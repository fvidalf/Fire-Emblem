using Fire_Emblem.Skills.SkillEffectFiles;
namespace Fire_Emblem.CharacterFiles.StatFiles;

public class StatModifier {
    
    private Dictionary<EffectType, int> _modifiers;
    
    public StatModifier() {
        InitializeDictionary();
    }
    
    private void InitializeDictionary() {
        _modifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0},
            {EffectType.FirstAttackBonus, 0},
            {EffectType.FollowUpBonus, 0},
            {EffectType.RegularPenalty, 0},
            {EffectType.FirstAttackPenalty, 0},
            {EffectType.FollowUpPenalty, 0}
        };
    }

    public void UpdateModifier(EffectType effectType, int amount) {
        _modifiers[effectType] += amount;
    }
    
    public int GetModifier(EffectType effectType) {
        return _modifiers[effectType];
    }
    
    public void ResetPenalties() {
        _modifiers[EffectType.RegularPenalty] = 0;
        _modifiers[EffectType.FirstAttackPenalty] = 0;
        _modifiers[EffectType.FollowUpPenalty] = 0;
    }
    
    public void ResetBonuses() {
        _modifiers[EffectType.RegularBonus] = 0;
        _modifiers[EffectType.FirstAttackBonus] = 0;
        _modifiers[EffectType.FollowUpBonus] = 0;
    }
}