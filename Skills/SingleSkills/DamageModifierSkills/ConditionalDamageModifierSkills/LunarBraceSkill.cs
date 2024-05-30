using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.DamageModifiersFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.DamageModifierSkills.ConditionalDamageModifierSkills;

public class LunarBraceSkill: ConditionalDamageModifierSkill {

    public LunarBraceSkill() : base("Lunar Brace",
        new DamageModification(EffectType.RegularDamageIncrease, 0)
    ) {}

    public override void Apply(RoundStatus roundStatus) {
        RoundStatus = roundStatus;
        DetermineTarget();
        SetDamageIncrease();
        if (IsConditionMet()) {
            Character.UpdateDamageModifiers(DamageModification);
        }
        
    }
    
    private void SetDamageIncrease() {
        var rivalDef = RoundStatus.RivalCharacterModel.Def;
        var damageIncrease = Convert.ToInt32(Math.Floor(rivalDef * 0.3));
        DamageModification.SetAmount(damageIncrease);
    }
    
    protected override bool IsConditionMet() {
        var isActivatingFirstToAttack = RoundStatus.ActivatingCharacterModel == RoundStatus.FirstCharacterModel;
        var isPhysical = RoundStatus.ActivatingCharacterModel.IsPhysical();
        return isActivatingFirstToAttack && isPhysical;
    }
    
}