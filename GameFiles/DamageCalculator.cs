using Fire_Emblem.CharacterFiles;

namespace Fire_Emblem.GameFiles;

public static class DamageCalculator {
    public static int CalculateFinalDamage(CharacterModel attacker, CharacterModel target, int roundPhase) {
        var damage = CalculateOriginalDamage(attacker, target, roundPhase);
        var finalDamage = CalculateDamageWithDiscounts(damage, attacker, target, roundPhase);
        return finalDamage;
    }
    
    private static int GetThisTurnAtk(CharacterModel attacker, int roundPhase) {
        var thisTurnAtk = attacker.Atk;
        if (attacker.FirstAttackAtk != 0 && roundPhase != 2) {
            thisTurnAtk += attacker.FirstAttackAtk;
        } else if (roundPhase == 2) {
            thisTurnAtk += attacker.FollowUpAtk;
        }
        return thisTurnAtk;
    }
    
    private static int GetThisTurnDef(CharacterModel target, int roundPhase) {
        var thisTurnDef = target.Def;
        if (target.FirstAttackDef != 0 && roundPhase != 2) {
            thisTurnDef += target.FirstAttackDef;
        } else if (roundPhase == 2) {
            thisTurnDef += target.FollowUpDef;
        }
        return thisTurnDef;
    }
    
    private static int GetThisTurnRes(CharacterModel target, int roundPhase) {
        var thisTurnRes = target.Res;
        if (target.FirstAttackRes != 0 && roundPhase != 2) {
            thisTurnRes += target.FirstAttackRes;
        } else if (roundPhase == 2) {
            thisTurnRes += target.FollowUpRes;
        }
        return thisTurnRes;
    }
    
    public static int CalculateOriginalDamage(CharacterModel attacker, CharacterModel target, int roundPhase) {
        var thisTurnAtk = GetThisTurnAtk(attacker, roundPhase);
        var thisTurnDef = GetThisTurnDef(target, roundPhase);
        var thisTurnRes = GetThisTurnRes(target, roundPhase);
        var discount = attacker.IsPhysical() ? thisTurnDef : thisTurnRes;
        var damage = Math.Max((Convert.ToInt32(Math.Floor(thisTurnAtk * GetWeaponTriangleAdvantage(attacker, target))) - discount), 0);
        return damage;
    }
    
    private static double GetWeaponTriangleAdvantage(CharacterModel attacker, CharacterModel target) {
        return WeaponTriangleAdvantage.GetAdvantage(attacker, target);
    }

    public static int CalculateDamageWithDiscounts(int damage, CharacterModel attacker, CharacterModel target, int roundPhase) {
        var absoluteDamageModifier = GetAbsoluteDamageModifier(attacker, target, roundPhase);
        var damageWithAbsoluteModifier = Math.Max(damage + absoluteDamageModifier, 0);
        var percentageDamageModifier = GetPercentageDamageModifier(target, roundPhase);
        var damageWithPercentageModifier = damageWithAbsoluteModifier * (1 - percentageDamageModifier);
        var roundedDamage = Math.Round(damageWithPercentageModifier, 9);
        var finalDamage = Convert.ToInt32(Math.Floor(roundedDamage));
        return finalDamage;
    }

    private static double GetAbsoluteDamageModifier(CharacterModel attacker, CharacterModel target, int roundPhase) {
        var attackerDamageIncrease = GetAttackerAbsoluteDamageIncrease(attacker, roundPhase);
        var targetDamageReduction = GetTargetAbsoluteDamageReduction(target);
        return attackerDamageIncrease - targetDamageReduction;
    }

    private static double GetAttackerAbsoluteDamageIncrease(CharacterModel attacker, int roundPhase) {
        double absoluteDamageIncrease = 0;
        var damageModifiers = attacker.GetDamageModifiers();
        absoluteDamageIncrease += damageModifiers.GetRegularDamageIncrease();
        if (damageModifiers.GetFirstAttackDamageIncrease() != 0 && roundPhase != 2) {
            absoluteDamageIncrease += damageModifiers.GetFirstAttackDamageIncrease();
        } else if (roundPhase == 2) {
            absoluteDamageIncrease += damageModifiers.GetFollowUpDamageIncrease();
        }
        return absoluteDamageIncrease;
    }
    
    private static double GetTargetAbsoluteDamageReduction(CharacterModel target) {
        double absoluteDamageReduction = 0;
        var damageModifiers = target.GetDamageModifiers();
        absoluteDamageReduction += damageModifiers.GetRegularDamageAbsoluteReduction();
        return absoluteDamageReduction;
    }
    
    private static double GetPercentageDamageModifier(CharacterModel target, int roundPhase) {
        double percentageDamageModifier = 0;
        var damageModifiers = target.GetDamageModifiers();
        var regularAmount = damageModifiers.GetRegularDamagePercentageReduction();
        percentageDamageModifier = (double) 1 - (1 - percentageDamageModifier) * (1 - regularAmount);
        if (damageModifiers.GetFirstAttackDamagePercentageReduction() != 0 && roundPhase != 2) {
            var firstAttackAmount = damageModifiers.GetFirstAttackDamagePercentageReduction();
            percentageDamageModifier = (double) 1 - (1 - percentageDamageModifier) * (1 - firstAttackAmount);
        } else if (roundPhase == 2) {
            var followUpAmount = damageModifiers.GetFollowUpDamagePercentageReduction();
            percentageDamageModifier = (double) 1 - ((1 - percentageDamageModifier) * (1 - followUpAmount));
        }
        return percentageDamageModifier;
    }
    
}