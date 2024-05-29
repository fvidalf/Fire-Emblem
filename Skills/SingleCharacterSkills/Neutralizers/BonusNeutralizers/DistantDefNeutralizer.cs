using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.Skills.SingleCharacterSkills.Neutralizers.BonusNeutralizers;

public class DistantDefNeutralizer: BonusNeutralizer {
    
    public DistantDefNeutralizer()
        : base("Distant Def Neutralize",
            new List<Stat> {Stat.AtkBonus, Stat.SpdBonus, Stat.ResBonus, Stat.DefBonus}) {}
    
    protected override void ConcreteApply(RoundStatus roundStatus) {
        if (IsConditionMet()) {
            SetResponseForNeutralizing(EffectType.BonusNeutralizer);
            NeutralizeStats();
        }
    }
    
    protected bool IsConditionMet() {
        var isRivalFirstToAttack = RoundStatus.RivalCharacterModel == RoundStatus.FirstCharacterModel;
        var isRivalUsingBowOrMagic = RoundStatus.RivalCharacterModel.Weapon == "Bow" || RoundStatus.RivalCharacterModel.Weapon == "Magic";
        return isRivalFirstToAttack && isRivalUsingBowOrMagic;
    }
}