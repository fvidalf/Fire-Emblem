using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.GameFiles;

public static class DamageCalculator {
    public static int CalculateFinalDamage(CharacterModel attacker, CharacterModel target, int roundPhase) {
        var damage = CalculateOriginalDamage(attacker, target, roundPhase);
        var damageWithExtras = GetDamageIncreases(attacker, roundPhase) + damage;
        var damageWithPercentDiscount = damageWithExtras * (1 - GetPercentageDamageModifier(target, roundPhase));
        var damageWithAbsoluteDiscount = damageWithPercentDiscount - GetAbsoluteDamageDiscounts(target);
        var roundedDamage = Math.Round(damageWithAbsoluteDiscount, 9);
        var finalDamage = Math.Max(Convert.ToInt32(Math.Floor(roundedDamage)), 0);
        return finalDamage;
    }
    
    public static int CalculateDamageWithOnlyIncreases(CharacterModel attacker, CharacterModel target, int roundPhase) {
        var originalDamage = CalculateOriginalDamage(attacker, target, roundPhase);
        var absoluteDamageModifier = GetDamageIncreases(attacker, roundPhase);
        var damageWithAbsoluteModifier = Math.Max(originalDamage + absoluteDamageModifier, 0);
        var finalDamage = Convert.ToInt32(Math.Floor(damageWithAbsoluteModifier));
        return finalDamage;
    }

    private static int CalculateOriginalDamage(CharacterModel attacker, CharacterModel target, int roundPhase) {
        var thisTurnAtk = GetThisTurnAtk(attacker, roundPhase);
        var thisTurnDef = GetThisTurnDef(target, roundPhase);
        var thisTurnRes = GetThisTurnRes(target, roundPhase);
        var discount = attacker.IsPhysical() ? thisTurnDef : thisTurnRes;
        var damage = Math.Max((Convert.ToInt32(Math.Floor(thisTurnAtk * GetWeaponTriangleAdvantage(attacker, target))) - discount), 0);
        return damage;
    }
    
    private static int GetThisTurnAtk(CharacterModel attacker, int roundPhase) {
        var thisTurnAtk = attacker.Atk;
        var turnBasedAtkBonus = roundPhase != 2 ? attacker.FirstAttackAtk : attacker.FollowUpAtk;
        thisTurnAtk += turnBasedAtkBonus;
        return thisTurnAtk;
    }
    
    private static int GetThisTurnDef(CharacterModel target, int roundPhase) {
        var thisTurnDef = target.Def;
        var turnBasedDefBonus = roundPhase != 2 ? target.FirstAttackDef : target.FollowUpDef;
        thisTurnDef += turnBasedDefBonus;
        return thisTurnDef;
    }
    
    private static int GetThisTurnRes(CharacterModel target, int roundPhase) {
        var thisTurnRes = target.Res;
        var turnBasedResBonus = roundPhase != 2 ? target.FirstAttackRes : target.FollowUpRes;
        thisTurnRes += turnBasedResBonus;
        return thisTurnRes;
    }
    
    private static double GetWeaponTriangleAdvantage(CharacterModel attacker, CharacterModel target) {
        return WeaponTriangleAdvantage.GetAdvantage(attacker, target);
    }
    
    private static double GetDamageIncreases(CharacterModel attacker, int roundPhase) {
        double absoluteDamageIncrease = 0;
        var damageModifiers = attacker.GetDamageModifiers();
        absoluteDamageIncrease += damageModifiers.GetDamageModifier(EffectType.RegularDamageIncrease);
        double turnBasedIncrease;
        if (roundPhase != 2) {
            turnBasedIncrease = damageModifiers.GetDamageModifier(EffectType.FirstAttackDamageIncrease);
        } else {
            turnBasedIncrease = damageModifiers.GetDamageModifier(EffectType.FollowUpDamageIncrease);
        }
        absoluteDamageIncrease += turnBasedIncrease;
        return absoluteDamageIncrease;
    }
    
    private static double GetPercentageDamageModifier(CharacterModel target, int roundPhase) {
        double percentageDamageModifier = 0;
        var damageModifiers = target.GetDamageModifiers();
        percentageDamageModifier = AddRegularDamagePercentageReduction(damageModifiers, percentageDamageModifier);
        if (roundPhase != 2) {
            percentageDamageModifier = AddFirstAttackDamagePercentageReduction(damageModifiers, percentageDamageModifier);
        } else {
            percentageDamageModifier = AddFollowUpDamagePercentageReduction(damageModifiers, percentageDamageModifier);
        }
        return percentageDamageModifier;
    }
    
    private static double AddRegularDamagePercentageReduction(DamageModifiers damageModifiers, double percentageDamageModifier) {
        var regularAmount = damageModifiers.GetDamageModifier(EffectType.RegularDamagePercentageReduction);
        return AddPercentageReduction(percentageDamageModifier, regularAmount);
    }
    
    private static double AddPercentageReduction(double originalValue, double percentageReduction) {
        return 1 - (1 - originalValue) * (1 - percentageReduction);
    }
    
    private static double AddFirstAttackDamagePercentageReduction(DamageModifiers damageModifiers, double percentageDamageModifier) {
        var firstAttackAmount = damageModifiers.GetDamageModifier(EffectType.FirstAttackDamagePercentageReduction);
        return AddPercentageReduction(percentageDamageModifier, firstAttackAmount);
    }
    
    private static double AddFollowUpDamagePercentageReduction(DamageModifiers damageModifiers, double percentageDamageModifier) {
        var followUpAmount = damageModifiers.GetDamageModifier(EffectType.FollowUpDamagePercentageReduction);
        return AddPercentageReduction(percentageDamageModifier, followUpAmount);
    }
    
    private static double GetAbsoluteDamageDiscounts(CharacterModel target) {
        double absoluteDamageReduction = 0;
        var damageModifiers = target.GetDamageModifiers();
        absoluteDamageReduction += damageModifiers.GetDamageModifier(EffectType.RegularDamageAbsoluteReduction);
        return absoluteDamageReduction;
    }
}