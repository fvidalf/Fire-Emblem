namespace Fire_Emblem.CharacterFiles;

public class CharacterInfo {
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public required string Name { get; set; }
    public required string Weapon { get; set; }
    public required string Gender { get; set; }
    public required string DeathQuote { get; set; }
    public required string HP { get; set; }
    public required string Atk { get; set; }
    public required string Spd { get; set; }
    public required string Def { get; set; }
    public required string Res { get; set; }
}