using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SkillsOverSelf.RegularSelfSkills.ConditionalSelfSkills.BrazenSkills;

public abstract class BrazenSkill: ConditionalSelfSkill {
    
    public BrazenSkill(string name, Dictionary<EffectType, List<StatEffect>> statsToModify)
        : base(name, statsToModify) {}

    protected override bool IsConditionMet() {
        var isHpAtOrBelow80 = Character.Hp <= Character.BaseHp * 0.8;
        return isHpAtOrBelow80;
    }
}