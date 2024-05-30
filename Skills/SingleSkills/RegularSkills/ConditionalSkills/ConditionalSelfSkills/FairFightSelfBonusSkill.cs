using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

public class FairFightSelfBonusSkill: ConditionalSelfSkill {
    
    public FairFightSelfBonusSkill()
        : base("Attack +6", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                new StatEffect(Stat.Atk, 6)
            }}
        }) {}
    
    protected override bool IsConditionMet() {
        var isActivatingFirstToAttack = RoundStatus.ActivatingCharacterModel == RoundStatus.FirstCharacterModel;
        return isActivatingFirstToAttack;
    }
}