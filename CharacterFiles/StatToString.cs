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
        };
    }
}