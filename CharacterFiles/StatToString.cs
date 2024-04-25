namespace Fire_Emblem.CharacterFiles
{
    public static class StatToString
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
        };
        
        public static readonly Dictionary<Stat, string> RegularizeMap = new Dictionary<Stat, string>
        {
            { Stat.FirstAttackAtk, "Atk" },
            { Stat.FirstAttackSpd, "Spd" },
            { Stat.FirstAttackDef, "Def" },
            { Stat.FirstAttackRes, "Res" },
            { Stat.HpBonus, "Hp"},
            { Stat.AtkBonus, "Atk"},
            { Stat.DefBonus, "Def"},
            { Stat.ResBonus, "Res"},
            { Stat.HpPenalty, "Hp"},
            { Stat.SpdPenalty, "Spd"},
            { Stat.DefPenalty, "Def"},
            { Stat.ResPenalty, "Res"},
        };
    }
}