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
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpAttackBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpAttackPenalty, 0}
        };
        AtkModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpAttackBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpAttackPenalty, 0}
        };
        SpdModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpAttackBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpAttackPenalty, 0}
        };
        DefModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpAttackBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpAttackPenalty, 0}
        };
        ResModifiers = new Dictionary<EffectType, int>() {
            {EffectType.RegularBonus, 0}, {EffectType.FirstAttackBonus, 0}, {EffectType.FollowUpAttackBonus, 0}, {EffectType.RegularPenalty, 0}, {EffectType.FirstAttackPenalty, 0}, {EffectType.FollowUpAttackPenalty, 0}
        };
    }
    
    public void UpdateModifiers(EffectType effectType, StatEffect statEffect) {
        switch (statEffect.Stat) {
            case Stat.Hp:
                HpModifiers[effectType] += statEffect.Amount;
                break;
            case Stat.Atk:
                AtkModifiers[effectType] += statEffect.Amount;
                break;
            case Stat.Spd:
                SpdModifiers[effectType] += statEffect.Amount;
                break;
            case Stat.Def:
                DefModifiers[effectType] += statEffect.Amount;
                break;
            case Stat.Res:
                ResModifiers[effectType] += statEffect.Amount;
                break;
        }
    }
    
    public void NeutralizeStats(Character character, List<Stat> statsToNeutralize) {
        foreach (var stat in statsToNeutralize) {
            switch (stat) {
                case Stat.HpPenalty:
                    NeutralizeHpPenalty(character);
                    break;
                case Stat.AtkPenalty:
                    NeutralizeAtkPenalty(character);
                    break;
                case Stat.SpdPenalty:
                    NeutralizeSpdPenalty(character);
                    break;
                case Stat.DefPenalty:
                    NeutralizeDefPenalty(character);
                    break;
                case Stat.ResPenalty:
                    NeutralizeResPenalty(character);
                    break;
                case Stat.HpBonus:
                    NeutralizeHpBonus(character);
                    break;
                case Stat.AtkBonus:
                    NeutralizeAtkBonus(character);
                    break;
                case Stat.SpdBonus:
                    NeutralizeSpdBonus(character);
                    break;
                case Stat.DefBonus:
                    NeutralizeDefBonus(character);
                    break;
                case Stat.ResBonus:
                    NeutralizeResBonus(character);
                    break;
            }
        }
    }

    private void NeutralizeHpPenalty(Character character) {
        character.Hp += int.Abs(HpModifiers[EffectType.RegularPenalty]);
        character.FirstAttackHp += int.Abs(HpModifiers[EffectType.FirstAttackPenalty]);
        HpModifiers[EffectType.RegularPenalty] = 0;
        HpModifiers[EffectType.FirstAttackPenalty] = 0;
    }
    
    private void NeutralizeAtkPenalty(Character character) {
        character.Atk += int.Abs(AtkModifiers[EffectType.RegularPenalty]);
        character.FirstAttackAtk += int.Abs(AtkModifiers[EffectType.FirstAttackPenalty]);
        AtkModifiers[EffectType.RegularPenalty] = 0;
        AtkModifiers[EffectType.FirstAttackPenalty] = 0;
    }
    
    private void NeutralizeSpdPenalty(Character character) {
        character.Spd += int.Abs(SpdModifiers[EffectType.RegularPenalty]);
        character.FirstAttackSpd += int.Abs(SpdModifiers[EffectType.FirstAttackPenalty]);
        SpdModifiers[EffectType.RegularPenalty] = 0;
        SpdModifiers[EffectType.FirstAttackPenalty] = 0;
    }
    
    private void NeutralizeDefPenalty(Character character) {
        character.Def += int.Abs(DefModifiers[EffectType.RegularPenalty]);
        character.FirstAttackDef += int.Abs(DefModifiers[EffectType.FirstAttackPenalty]);
        DefModifiers[EffectType.RegularPenalty] = 0;
        DefModifiers[EffectType.FirstAttackPenalty] = 0;
    }
    
    private void NeutralizeResPenalty(Character character) {
        character.Res += int.Abs(ResModifiers[EffectType.RegularPenalty]);
        character.FirstAttackRes += int.Abs(ResModifiers[EffectType.FirstAttackPenalty]);
        ResModifiers[EffectType.RegularPenalty] = 0;
        ResModifiers[EffectType.FirstAttackPenalty] = 0;
    }
    
    private void NeutralizeHpBonus(Character character) {
        character.Hp -= int.Abs(HpModifiers[EffectType.RegularBonus]);
        character.FirstAttackHp -= int.Abs(HpModifiers[EffectType.FirstAttackBonus]);
        HpModifiers[EffectType.RegularBonus] = 0;
        HpModifiers[EffectType.FirstAttackBonus] = 0;
    }
    
    private void NeutralizeAtkBonus(Character character) {
        character.Atk -= int.Abs(AtkModifiers[EffectType.RegularBonus]);
        character.FirstAttackAtk -= int.Abs(AtkModifiers[EffectType.FirstAttackBonus]);
        AtkModifiers[EffectType.RegularBonus] = 0;
        AtkModifiers[EffectType.FirstAttackBonus] = 0;
    }
    
    private void NeutralizeSpdBonus(Character character) {
        character.Spd -= int.Abs(SpdModifiers[EffectType.RegularBonus]);
        character.FirstAttackSpd -= int.Abs(SpdModifiers[EffectType.FirstAttackBonus]);
        SpdModifiers[EffectType.RegularBonus] = 0;
        SpdModifiers[EffectType.FirstAttackBonus] = 0;
    }
    
    private void NeutralizeDefBonus(Character character) {
        character.Def -= int.Abs(DefModifiers[EffectType.RegularBonus]);
        character.FirstAttackDef -= int.Abs(DefModifiers[EffectType.FirstAttackBonus]);
        DefModifiers[EffectType.RegularBonus] = 0;
        DefModifiers[EffectType.FirstAttackBonus] = 0;
    }
    
    private void NeutralizeResBonus(Character character) {
        character.Res -= int.Abs(ResModifiers[EffectType.RegularBonus]);
        character.FirstAttackRes -= int.Abs(ResModifiers[EffectType.FirstAttackBonus]);
        ResModifiers[EffectType.RegularBonus] = 0;
        ResModifiers[EffectType.FirstAttackBonus] = 0;
    }
    
    
}