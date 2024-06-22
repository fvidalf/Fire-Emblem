using Fire_Emblem_View;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills;
using Fire_Emblem.Skills.SingleSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.CharacterFiles;

public class CharacterHandler {
    
    private static View _view;
    
    public CharacterHandler(View view) {
        _view = view;
    }

    public void Attack(CharacterModel attacker, CharacterModel target, int roundPhase) {
        ExecuteAttack(attacker, target, roundPhase);
        attacker.SetMostRecentRival(target);
    }

    private static void ExecuteAttack(CharacterModel attacker, CharacterModel target, int roundPhase) {
        var finalDamage = DamageCalculator.CalculateFinalDamage(attacker, target, roundPhase);
        target.Hp -= finalDamage;
        _view.WriteLine($"{attacker.Name} ataca a {target.Name} con {finalDamage} de daño");

        ResetUsedModifiers(attacker, target, roundPhase);
    }
    
    private static void ResetUsedModifiers(CharacterModel attacker, CharacterModel target, int roundPhase) {
        ResetAtkModifiers(attacker, roundPhase);
        ResetDefModifiers(target, roundPhase);
        ResetResModifiers(target, roundPhase);
        ResetAbsoluteDamageModifiers(attacker, roundPhase);
        ResetPercentageDamageModifiers(target, roundPhase);
    }
    
    private static void ResetAtkModifiers(CharacterModel attacker, int roundPhase) {
        attacker.FirstAttackAtk = 0;
        if (roundPhase == 2) {
            attacker.FollowUpAtk = 0;
        }
    }
    
    private static void ResetDefModifiers(CharacterModel target, int roundPhase) {
        target.FirstAttackDef = 0;
        if (roundPhase == 2) {
            target.FollowUpDef = 0;
        }
    }
    
    private static void ResetResModifiers(CharacterModel target, int roundPhase) {
        target.FirstAttackRes = 0;
        if (roundPhase == 2) {
            target.FollowUpRes = 0;
        }
    }
    
    private static void ResetAbsoluteDamageModifiers(CharacterModel attacker, int roundPhase) {
        var attackerDamageModifiers = attacker.GetDamageModifiers();
        attackerDamageModifiers.ResetDamageModifier(EffectType.FirstAttackDamageIncrease);
        if (roundPhase == 2) {
            attackerDamageModifiers.ResetDamageModifier(EffectType.FollowUpDamageIncrease);
        }
    }

    private static void ResetPercentageDamageModifiers(CharacterModel target, int roundPhase) {
        var targetDamageModifiers = target.GetDamageModifiers();
        targetDamageModifiers.ResetDamageModifier(EffectType.FirstAttackDamagePercentageReduction);
        if (roundPhase == 2) {
            targetDamageModifiers.ResetDamageModifier(EffectType.FollowUpDamagePercentageReduction);
        }
    }
    
    public void ApplySkill(CharacterModel applier, IBaseSkill skill, RoundStatus roundStatus) {
        skill.Apply(roundStatus);
        if (skill is StatModifierSkill modifierSkill) {
            var characterPairedToSkillEffect = GetStatsModifiedBySkill(modifierSkill);
            UpdateModifiedStats(applier, characterPairedToSkillEffect);
        }
    }
    
    private Dictionary<CharacterModel, SkillEffect> GetStatsModifiedBySkill(StatModifierSkill skill) {
        return skill.GetModifiedStats();
    }
    
    private void UpdateModifiedStats(CharacterModel applier, Dictionary<CharacterModel, SkillEffect> characterPairedToSkillEffect) {
        foreach (var pair in characterPairedToSkillEffect) {
            var character = pair.Key;
            var skillEffect = pair.Value;
            if (character == applier) {
                applier.UpdateSelfModifiedStats(skillEffect);
            }
            else {
                applier.UpdateRivalModifiedStats(skillEffect);
            }
        }
    }
}