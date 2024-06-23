using Fire_Emblem_View;
using Fire_Emblem.CharacterFiles;
using Fire_Emblem.Skills;
using Fire_Emblem.Skills.SkillCollections;

namespace Fire_Emblem.TeamsLoaderFiles;

public static class CharacterLoader {
    
    public static CharacterModel CreateCharacterFromInfo(List<CharacterInfo> charactersInfo, string unitName, BaseSkillCollection unitBaseSkills, View view) {
        var characterInfo = charactersInfo.Find(character => character.Name.Trim() == unitName.Trim());
        var character = new CharacterModel(
            characterInfo.Name,
            characterInfo.Weapon,
            characterInfo.Gender,
            characterInfo.DeathQuote,
            unitBaseSkills,
            Convert.ToInt32(characterInfo.HP),
            Convert.ToInt32(characterInfo.Atk),
            Convert.ToInt32(characterInfo.Spd),
            Convert.ToInt32(characterInfo.Def),
            Convert.ToInt32(characterInfo.Res)
        );
        return character;
    }
}