using Fire_Emblem.Skills.SkillEffectFiles;

namespace Fire_Emblem.CharacterFiles.StatFiles;

public class StatModifiers {
    
    public Dictionary<EffectType, int> HpModifiers;
    public Dictionary<EffectType, int> AtkModifiers;
    public Dictionary<EffectType, int> SpdModifiers;
    public Dictionary<EffectType, int> DefModifiers;
    public Dictionary<EffectType, int> ResModifiers;

    public StatModifiers() {
        InitializeModifierMemory();
    }
    
    private void InitializeModifierMemory() {
        HpModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpPenalty, 0}
        };
        AtkModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpPenalty, 0}
        };
        SpdModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpPenalty, 0}
        };
        DefModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpPenalty, 0}
        };
        ResModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpPenalty, 0}
        };
    }
    
    public void UpdateModifiers(EffectType effectType, StatEffect statEffect) {
        switch (statEffect.Stat) {
            case Stat.Hp:
                HpModifiers[effectType] += statEffect.Amount;
                break;
            case Stat.Atk or Stat.FirstAttackAtk or Stat.FollowUpAtk:
                AtkModifiers[effectType] += statEffect.Amount;
                break;
            case Stat.Spd or Stat.FirstAttackSpd:
                SpdModifiers[effectType] += statEffect.Amount;
                break;
            case Stat.Def or Stat.FirstAttackDef or Stat.FollowUpDef:
                DefModifiers[effectType] += statEffect.Amount;
                break;
            case Stat.Res or Stat.FirstAttackRes or Stat.FollowUpRes:
                ResModifiers[effectType] += statEffect.Amount;
                break;
        }
    }
    
    public void NeutralizeStats(CharacterModel characterModel, List<Stat> statsToNeutralize) {
        foreach (var stat in statsToNeutralize) {
            switch (stat) {
                case Stat.HpPenalty:
                    NeutralizeHpPenalty(characterModel);
                    break;
                case Stat.AtkPenalty:
                    NeutralizeAtkPenalty(characterModel);
                    break;
                case Stat.SpdPenalty:
                    NeutralizeSpdPenalty(characterModel);
                    break;
                case Stat.DefPenalty:
                    NeutralizeDefPenalty(characterModel);
                    break;
                case Stat.ResPenalty:
                    NeutralizeResPenalty(characterModel);
                    break;
                case Stat.HpBonus:
                    NeutralizeHpBonus(characterModel);
                    break;
                case Stat.AtkBonus:
                    NeutralizeAtkBonus(characterModel);
                    break;
                case Stat.SpdBonus:
                    NeutralizeSpdBonus(characterModel);
                    break;
                case Stat.DefBonus:
                    NeutralizeDefBonus(characterModel);
                    break;
                case Stat.ResBonus:
                    NeutralizeResBonus(characterModel);
                    break;
            }
        }
    }

    private void NeutralizeHpPenalty(CharacterModel characterModel) {
        characterModel.Hp += int.Abs(HpModifiers[EffectType.RegularPenalty]);
        characterModel.FirstAttackHp += int.Abs(HpModifiers[EffectType.FirstAttackPenalty]);
        HpModifiers[EffectType.RegularPenalty] = 0;
        HpModifiers[EffectType.FirstAttackPenalty] = 0;
    }
    
    private void NeutralizeAtkPenalty(CharacterModel characterModel) {
        characterModel.Atk += int.Abs(AtkModifiers[EffectType.RegularPenalty]);
        characterModel.FirstAttackAtk += int.Abs(AtkModifiers[EffectType.FirstAttackPenalty]);
        characterModel.FollowUpAtk += int.Abs(AtkModifiers[EffectType.FollowUpPenalty]);
        AtkModifiers[EffectType.RegularPenalty] = 0;
        AtkModifiers[EffectType.FirstAttackPenalty] = 0;
        AtkModifiers[EffectType.FollowUpPenalty] = 0;
    }
    
    private void NeutralizeSpdPenalty(CharacterModel characterModel) {
        characterModel.Spd += int.Abs(SpdModifiers[EffectType.RegularPenalty]);
        characterModel.FirstAttackSpd += int.Abs(SpdModifiers[EffectType.FirstAttackPenalty]);
        SpdModifiers[EffectType.RegularPenalty] = 0;
        SpdModifiers[EffectType.FirstAttackPenalty] = 0;
    }
    
    private void NeutralizeDefPenalty(CharacterModel characterModel) {
        characterModel.Def += int.Abs(DefModifiers[EffectType.RegularPenalty]);
        characterModel.FirstAttackDef += int.Abs(DefModifiers[EffectType.FirstAttackPenalty]);
        characterModel.FollowUpDef += int.Abs(DefModifiers[EffectType.FollowUpPenalty]);
        DefModifiers[EffectType.RegularPenalty] = 0;
        DefModifiers[EffectType.FirstAttackPenalty] = 0;
        DefModifiers[EffectType.FollowUpPenalty] = 0;
    }
    
    private void NeutralizeResPenalty(CharacterModel characterModel) {
        characterModel.Res += int.Abs(ResModifiers[EffectType.RegularPenalty]);
        characterModel.FirstAttackRes += int.Abs(ResModifiers[EffectType.FirstAttackPenalty]);
        characterModel.FollowUpRes += int.Abs(ResModifiers[EffectType.FollowUpPenalty]);
        ResModifiers[EffectType.RegularPenalty] = 0;
        ResModifiers[EffectType.FirstAttackPenalty] = 0;
        ResModifiers[EffectType.FollowUpPenalty] = 0;
    }
    
    private void NeutralizeHpBonus(CharacterModel characterModel) {
        characterModel.Hp -= int.Abs(HpModifiers[EffectType.RegularBonus]);
        characterModel.FirstAttackHp -= int.Abs(HpModifiers[EffectType.FirstAttackBonus]);
        HpModifiers[EffectType.RegularBonus] = 0;
        HpModifiers[EffectType.FirstAttackBonus] = 0;
    }
    
    private void NeutralizeAtkBonus(CharacterModel characterModel) {
        characterModel.Atk -= int.Abs(AtkModifiers[EffectType.RegularBonus]);
        characterModel.FirstAttackAtk -= int.Abs(AtkModifiers[EffectType.FirstAttackBonus]);
        characterModel.FollowUpAtk -= int.Abs(AtkModifiers[EffectType.FollowUpBonus]);
        AtkModifiers[EffectType.RegularBonus] = 0;
        AtkModifiers[EffectType.FirstAttackBonus] = 0;
        AtkModifiers[EffectType.FollowUpBonus] = 0;
    }
    
    private void NeutralizeSpdBonus(CharacterModel characterModel) {
        characterModel.Spd -= int.Abs(SpdModifiers[EffectType.RegularBonus]);
        characterModel.FirstAttackSpd -= int.Abs(SpdModifiers[EffectType.FirstAttackBonus]);
        SpdModifiers[EffectType.RegularBonus] = 0;
        SpdModifiers[EffectType.FirstAttackBonus] = 0;
    }
    
    private void NeutralizeDefBonus(CharacterModel characterModel) {
        characterModel.Def -= int.Abs(DefModifiers[EffectType.RegularBonus]);
        characterModel.FirstAttackDef -= int.Abs(DefModifiers[EffectType.FirstAttackBonus]);
        characterModel.FollowUpDef -= int.Abs(DefModifiers[EffectType.FollowUpBonus]);
        DefModifiers[EffectType.RegularBonus] = 0;
        DefModifiers[EffectType.FirstAttackBonus] = 0;
        DefModifiers[EffectType.FollowUpBonus] = 0;
    }
    
    private void NeutralizeResBonus(CharacterModel characterModel) {
        characterModel.Res -= int.Abs(ResModifiers[EffectType.RegularBonus]);
        characterModel.FirstAttackRes -= int.Abs(ResModifiers[EffectType.FirstAttackBonus]);
        characterModel.FollowUpRes -= int.Abs(ResModifiers[EffectType.FollowUpBonus]);
        ResModifiers[EffectType.RegularBonus] = 0;
        ResModifiers[EffectType.FirstAttackBonus] = 0;
        ResModifiers[EffectType.FollowUpBonus] = 0;
    }
}