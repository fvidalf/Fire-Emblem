using Fire_Emblem.CharacterFiles.StatFiles;
using Fire_Emblem.GameFiles;
using Fire_Emblem.Skills.SkillEffectFiles;
namespace Fire_Emblem.Skills.SingleSkills.RegularSkills.ConditionalSkills.ConditionalRivalSkills;

public class FlarePenaltySkill : ConditionalRivalSkill {
        
        public FlarePenaltySkill()
            : base("Flare Penalty", new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularPenalty, new List<StatEffect> {
                    new StatEffect(Stat.Spd, -4),
                }}
            }) {}
        
        protected override void ConcreteApply(RoundStatus roundStatus) {
            DetermineTarget();
            SetStatsToModify();
            if (IsConditionMet()) {
                foreach (KeyValuePair<EffectType, List<StatEffect>> statEffects in StatsToModify) {
                    var effectType = statEffects.Key;
                    foreach (var statEffect in statEffects.Value) {
                        UpdateStat(Character, effectType, statEffect);
                    }
                }
            }
        }
        
        private void SetStatsToModify() {
            var resPenalty = (int) - (RoundStatus.RivalCharacterModel.Res * 0.2);
            StatsToModify = new Dictionary<EffectType, List<StatEffect>> {
                {EffectType.RegularPenalty, new List<StatEffect> {
                    new StatEffect(Stat.Res, resPenalty),
                }}
            };
        }
    
        protected override bool IsConditionMet() {
            var isActivatingMagicUser = RoundStatus.ActivatingCharacterModel.Weapon == "Magic";
            return isActivatingMagicUser;
        }
}