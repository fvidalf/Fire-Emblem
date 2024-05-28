using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers;

public class CloseDefNeutralizer: BonusNeutralizer {
    
    public CloseDefNeutralizer()
        : base("Close Def Neutralize",
            new List<Stat> {Stat.AtkBonus, Stat.SpdBonus, Stat.ResBonus, Stat.DefBonus}) {}
    
    protected override void ConcreteApply(RoundStatus roundStatus) {
        if (IsConditionMet()) {
            SetResponseForNeutralizing(EffectType.BonusNeutralizer);
            NeutralizeStats();
        }
    }
    
    protected bool IsConditionMet() {
        var isRivalFirstToAttack = RoundStatus.RivalCharacterModel == RoundStatus.FirstCharacterModel;
        var isRivalUsingSwordLanceOrAxe = RoundStatus.RivalCharacterModel.Weapon == "Sword" || RoundStatus.RivalCharacterModel.Weapon == "Lance" || RoundStatus.RivalCharacterModel.Weapon == "Axe";
        return isRivalFirstToAttack && isRivalUsingSwordLanceOrAxe;
    }
}