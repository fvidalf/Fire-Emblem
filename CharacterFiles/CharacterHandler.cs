﻿using Fire_Emblem_View;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills;
using Fire_Emblem.Skills.SingleCharacterSkills;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.CharacterFiles;

public class CharacterHandler {
    
    private static View _view;
    
    public CharacterHandler(View view) {
        _view = view;
    }
    
    public void Attack(CharacterModel attacker, CharacterModel target) {
        var thisTurnAtk = GetThisTurnAtk(attacker);
        var thisTurnDef = GetThisTurnDef(target);
        var thisTurnRes = GetThisTurnRes(target);
        ExecuteAttack(attacker, thisTurnAtk, thisTurnDef, thisTurnRes, target);
        attacker.SetMostRecentRival(target);
    }
    
    private static int GetThisTurnAtk(CharacterModel attacker) {
        var thisTurnAtk = attacker.Atk;
        if (attacker.FirstAttackAtk != 0) {
            thisTurnAtk += attacker.FirstAttackAtk;
            attacker.FirstAttackAtk = 0;
        }
        return thisTurnAtk;
    }
    
    private static int GetThisTurnDef(CharacterModel target) {
        var thisTurnDef = target.Def;
        if (target.FirstAttackDef != 0) {
            thisTurnDef += target.FirstAttackDef;
            target.FirstAttackDef = 0;
        }
        return thisTurnDef;
    }
    
    private static int GetThisTurnRes(CharacterModel target) {
        var thisTurnRes = target.Res;
        if (target.FirstAttackRes != 0) {
            thisTurnRes += target.FirstAttackRes;
            target.FirstAttackRes = 0;
        }
        return thisTurnRes;
    }

    private static void ExecuteAttack(CharacterModel attacker, int thisTurnAtk, int thisTurnDef, int thisTurnRes, CharacterModel target) {
        var discount = attacker.IsPhysical() ? thisTurnDef : thisTurnRes;
        var damage = Math.Max((Convert.ToInt32(Math.Floor(thisTurnAtk * GetWeaponTriangleAdvantage(attacker, target))) - discount), 0);
        target.Hp -= damage;
        _view.WriteLine($"{attacker.Name} ataca a {target.Name} con {damage} de daño");
    }
    
    private static double GetWeaponTriangleAdvantage(CharacterModel attacker, CharacterModel target) {
        return WeaponTriangleAdvantage.GetAdvantage(attacker, target);
    }
    
    public void ApplySkill(CharacterModel applier, IBaseSkill skill, RoundStatus roundStatus) {
        skill.Apply(roundStatus);
        var characterPairedToSkillEffect = GetStatsModifiedBySkill((SingleCharacterSkill)skill);
        UpdateModifiedStats(applier, characterPairedToSkillEffect);
    }
    
    private Dictionary<CharacterModel, SkillEffect> GetStatsModifiedBySkill(IBaseSkill skill) {
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