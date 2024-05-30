using Fire_Emblem_View;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills;
using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SingleCharacterSkills.DamageModifierSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.CharacterFiles;

public class CharacterHandler {
    
    private static View _view;
    
    public CharacterHandler(View view) {
        _view = view;
    }
    
    public void Attack(CharacterModel attacker, CharacterModel target, int roundPhase) {
        var thisTurnAtk = GetThisTurnAtk(attacker, roundPhase);
        var thisTurnDef = GetThisTurnDef(target, roundPhase);
        var thisTurnRes = GetThisTurnRes(target, roundPhase);
        ExecuteAttack(attacker, thisTurnAtk, thisTurnDef, thisTurnRes, target);
        attacker.SetMostRecentRival(target);
    }
    
    private static int GetThisTurnAtk(CharacterModel attacker, int roundPhase) {
        var thisTurnAtk = attacker.Atk;
        if (attacker.FirstAttackAtk != 0 && roundPhase != 2) {
            thisTurnAtk += attacker.FirstAttackAtk;
            attacker.FirstAttackAtk = 0;
        } else if (roundPhase == 2) {
            thisTurnAtk += attacker.FollowUpAtk;
            attacker.FollowUpAtk = 0;
        }
        return thisTurnAtk;
    }
    
    private static int GetThisTurnDef(CharacterModel target, int roundPhase) {
        var thisTurnDef = target.Def;
        if (target.FirstAttackDef != 0 && roundPhase != 2) {
            thisTurnDef += target.FirstAttackDef;
            target.FirstAttackDef = 0;
        } else if (roundPhase == 2) {
            thisTurnDef += target.FollowUpDef;
            target.FollowUpDef = 0;
        }
        return thisTurnDef;
    }
    
    private static int GetThisTurnRes(CharacterModel target, int roundPhase) {
        var thisTurnRes = target.Res;
        if (target.FirstAttackRes != 0 && roundPhase != 2) {
            thisTurnRes += target.FirstAttackRes;
            target.FirstAttackRes = 0;
        } else if (roundPhase == 2) {
            thisTurnRes += target.FollowUpRes;
            target.FollowUpRes = 0;
        }
        return thisTurnRes;
    }

    private static void ExecuteAttack(CharacterModel attacker, int thisTurnAtk, int thisTurnDef, int thisTurnRes, CharacterModel target) {
        var discount = attacker.IsPhysical() ? thisTurnDef : thisTurnRes;
        var damage = Math.Max((Convert.ToInt32(Math.Floor(thisTurnAtk * GetWeaponTriangleAdvantage(attacker, target))) - discount), 0);
        var damageModifier = GetTotalDamageModifier(attacker, target, damage);
        var finalDamage = Math.Max(damage + damageModifier, 0);
        target.Hp -= finalDamage;
        _view.WriteLine($"{attacker.Name} ataca a {target.Name} con {finalDamage} de daño");
    }
    
    private static double GetWeaponTriangleAdvantage(CharacterModel attacker, CharacterModel target) {
        return WeaponTriangleAdvantage.GetAdvantage(attacker, target);
    }

    private static int GetTotalDamageModifier(CharacterModel attacker, CharacterModel target, int damage) {
        var attackerDamageIncrease = GetTotalAttackerDamageIncrease(attacker, damage);
        var targetDamageReduction = GetTotalTargetDamageReduction(target, damage);
        return attackerDamageIncrease - targetDamageReduction;
    }

    private static int GetTotalAttackerDamageIncrease(CharacterModel attacker, int damage) {
        var totalDamageIncrease = 0;
        var damageModifiers = attacker.GetDamageModifiers();
        foreach (var effectType in damageModifiers.DamageModifiersByEffectType.Keys) {
            var amount = damageModifiers.DamageModifiersByEffectType[effectType];
            switch (effectType) {
                case EffectType.RegularDamageIncrease:
                    totalDamageIncrease += amount;
                    break;
            }
        }
        return totalDamageIncrease;
    }
    
    private static int GetTotalTargetDamageReduction(CharacterModel target, int damage) {
        var totalDamageReduction = 0;
        var damageModifiers = target.GetDamageModifiers();
        foreach (var effectType in damageModifiers.DamageModifiersByEffectType.Keys) {
            var amount = damageModifiers.DamageModifiersByEffectType[effectType];
            switch (effectType) {
                case EffectType.RegularDamagePercentageReduction:
                    totalDamageReduction += Convert.ToInt32(Math.Floor((double) damage * amount));
                    break;
            }
        }
        return totalDamageReduction;
    }
    
    public void ApplySkill(CharacterModel applier, IBaseSkill skill, RoundStatus roundStatus) {
        Console.WriteLine($"{applier.Name}-{applier.Id} intenta aplicar {skill.Name}");
        skill.Apply(roundStatus);
        if (skill is StatModifierSkill modifierSkill) {
            var characterPairedToSkillEffect = GetStatsModifiedBySkill(modifierSkill);
            UpdateModifiedStats(applier, characterPairedToSkillEffect);
        }
    }
    
    private void UpdateDamageModifiers(CharacterModel applier, DamageModifierSkill skill) {
        throw new NotImplementedException();
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