using Fire_Emblem.CharacterFiles.StatFiles;

namespace Fire_Emblem.Skills.SingleSkills.Neutralizers.BonusNeutralizers;

public class LaguzFriendNeutralizerSkill: BonusNeutralizer {
    
    public LaguzFriendNeutralizerSkill()
        : base("Laguz Friend: Neutralizer",
            new List<Stat> {Stat.DefBonus, Stat.ResBonus}) {
    }
    
    public override void DetermineTarget() {
        Character = RoundStatus.ActivatingCharacterModel;
    }
    
}