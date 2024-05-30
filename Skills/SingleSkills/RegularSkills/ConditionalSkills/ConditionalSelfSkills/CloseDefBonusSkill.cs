using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalSelfSkills;

public class CloseDefBonusSkill: ConditionalSelfSkill {
    
    public CloseDefBonusSkill()
        : base("Close Def Bonus", new Dictionary<EffectType, List<StatEffect>> {
            {EffectType.RegularBonus, new List<StatEffect> {
                new StatEffect(Stat.Def, 8),
                new StatEffect(Stat.Res, 8)
            }}
        }) {}

    protected override bool IsConditionMet() {
        var isRivalFirstToAttack = RoundStatus.RivalCharacterModel == RoundStatus.FirstCharacterModel;
        var isRivalUsingSwordLanceOrAxe = RoundStatus.RivalCharacterModel.Weapon == "Sword" || RoundStatus.RivalCharacterModel.Weapon == "Lance" || RoundStatus.RivalCharacterModel.Weapon == "Axe";
        return isRivalFirstToAttack && isRivalUsingSwordLanceOrAxe;
    }
}