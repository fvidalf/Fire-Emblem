namespace Fire_Emblem.CharacterFiles.StatFiles
{
    public static class StatSimplifier
    {
        public static readonly Dictionary<Stat, string> Map = new Dictionary<Stat, string>
        {
            { Stat.Hp, "Hp" },
            { Stat.Atk, "Atk" },
            { Stat.Def, "Def" },
            { Stat.Spd, "Spd" },
            { Stat.Res, "Res" },
            { Stat.FirstAttackAtk, "FirstAttackAtk" },
            { Stat.FirstAttackSpd, "FirstAttackSpd" },
            { Stat.FirstAttackDef, "FirstAttackDef" },
            { Stat.FirstAttackRes, "FirstAttackRes" },
            { Stat.FollowUpAtk, "FollowUpAtk"},
            { Stat.FollowUpRes, "FollowUpRes"},
            { Stat.FollowUpDef, "FollowUpDef"}
        };
        
        public static readonly Dictionary<Stat, string> RegularizeMap = new Dictionary<Stat, string>
        {
            { Stat.FirstAttackAtk, "Atk" },
            { Stat.FirstAttackSpd, "Spd" },
            { Stat.FirstAttackDef, "Def" },
            { Stat.FirstAttackRes, "Res" },
            { Stat.FollowUpAtk, "Atk"},
            { Stat.FollowUpRes, "Def"},
            { Stat.FollowUpDef, "Res"},
            { Stat.HpBonus, "Hp"},
            { Stat.AtkBonus, "Atk"},
            { Stat.SpdBonus, "Spd"},
            { Stat.DefBonus, "Def"},
            { Stat.ResBonus, "Res"},
            { Stat.HpPenalty, "Hp"},
            { Stat.AtkPenalty, "Atk"},
            { Stat.SpdPenalty, "Spd"},
            { Stat.DefPenalty, "Def"},
            { Stat.ResPenalty, "Res"},
            { Stat.HpBonusByPercentage, "Hp"}
        };
        
        public static readonly Dictionary<Stat, Stat> SimplifyStatMap = new Dictionary<Stat, Stat>
        {
            { Stat.Atk, Stat.Atk },
            { Stat.Def, Stat.Def },
            { Stat.Spd, Stat.Spd },
            { Stat.Res, Stat.Res },
            { Stat.Hp, Stat.Hp },
            { Stat.FirstAttackAtk, Stat.Atk },
            { Stat.FirstAttackSpd, Stat.Spd },
            { Stat.FirstAttackDef, Stat.Def },
            { Stat.FirstAttackRes, Stat.Res },
            { Stat.FollowUpAtk, Stat.Atk },
            { Stat.FollowUpRes, Stat.Def },
            { Stat.FollowUpDef, Stat.Res },
            { Stat.HpBonus, Stat.Hp },
            { Stat.AtkBonus, Stat.Atk },
            { Stat.SpdBonus, Stat.Spd },
            { Stat.DefBonus, Stat.Def },
            { Stat.ResBonus, Stat.Res },
            { Stat.HpPenalty, Stat.Hp },
            { Stat.AtkPenalty, Stat.Atk },
            { Stat.SpdPenalty, Stat.Spd },
            { Stat.DefPenalty, Stat.Def },
            { Stat.ResPenalty, Stat.Res },
        };
        
        public static readonly Dictionary<Stat, bool> IsBonusMap = new Dictionary<Stat, bool>
        {
            { Stat.HpBonus, true },
            { Stat.AtkBonus, true },
            { Stat.SpdBonus, true },
            { Stat.DefBonus, true },
            { Stat.ResBonus, true },
            { Stat.HpPenalty, false },
            { Stat.AtkPenalty, false },
            { Stat.SpdPenalty, false },
            { Stat.DefPenalty, false },
            { Stat.ResPenalty, false },
        };
    }
}