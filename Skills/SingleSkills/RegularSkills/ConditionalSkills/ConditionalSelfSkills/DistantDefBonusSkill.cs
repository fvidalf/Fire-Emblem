using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

public class DistantDefBonusSkill: ConditionalSelfSkill {
    
    public DistantDefBonusSkill()
        : base("Distant Def Bonus", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                new StatEffect(Stat.Def, 8),
                new StatEffect(Stat.Res, 8)
            }}
        }) {}

    protected override bool IsConditionMet() {
        var isRivalFirstToAttack = RoundStatus.RivalCharacterModel == RoundStatus.FirstCharacterModel;
        var isRivalUsingBowOrMagic = RoundStatus.RivalCharacterModel.Weapon == "Bow" || RoundStatus.RivalCharacterModel.Weapon == "Magic";
        return isRivalFirstToAttack && isRivalUsingBowOrMagic;
    }
}