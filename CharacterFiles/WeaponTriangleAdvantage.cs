namespace Fire_Emblem.CharacterFiles;

public static class WeaponTriangleAdvantage {

    public static double GetAdvantage(CharacterModel self, CharacterModel target) {
        switch (self.Weapon) {
            case "Sword" when target.Weapon == "Axe":
            case "Axe" when target.Weapon == "Lance":
            case "Lance" when target.Weapon == "Sword":
                return 1.2d;
            case "Axe" when target.Weapon == "Sword":
            case "Lance" when target.Weapon == "Axe":
            case "Sword" when target.Weapon == "Lance":
                return 0.8d;
            default:
                return 1.0d;
        }
    }
    
    public static string GetAdvantageMessage(CharacterModel self, CharacterModel target) {
        switch (self.Weapon) {
            case "Sword" when target.Weapon == "Axe":
            case "Axe" when target.Weapon == "Lance":
            case "Lance" when target.Weapon == "Sword":
                return $"{self.Name} ({self.Weapon}) tiene ventaja con respecto a {target.Name} ({target.Weapon})";
            case "Axe" when target.Weapon == "Sword":
            case "Lance" when target.Weapon == "Axe":
            case "Sword" when target.Weapon == "Lance":
                return $"{target.Name} ({target.Weapon}) tiene ventaja con respecto a {self.Name} ({self.Weapon})";
            default:
                return "Ninguna unidad tiene ventaja con respecto a la otra";
        }
    }
}