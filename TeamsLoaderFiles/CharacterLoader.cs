using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills;

namespace Fire_Emblem.TeamsLoaderFiles;

public static class CharacterLoader {
    
    public static CharacterModel CreateCharacterFromInfo(List<CharacterInfo> charactersInfo, string unitName, IBaseSkill[] unitSkills, View view) {
        var characterInfo = charactersInfo.Find(character => character.Name.Trim() == unitName.Trim());
        var character = new CharacterModel(
            characterInfo.Name,
            characterInfo.Weapon,
            characterInfo.Gender,
            characterInfo.DeathQuote,
            unitSkills,
            Convert.ToInt32(characterInfo.HP),
            Convert.ToInt32(characterInfo.Atk),
            Convert.ToInt32(characterInfo.Spd),
            Convert.ToInt32(characterInfo.Def),
            Convert.ToInt32(characterInfo.Res),
            view
        );
        return character;
    }
}