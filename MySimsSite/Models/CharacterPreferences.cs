using Domain.Entities;

namespace MjauriziaSims.Models;

public class CharacterPreferences
{
    public IEnumerable<int>? Likes { get; set; }
    public IEnumerable<int>? Dislikes { get; set; }

    public static CharacterPreferences GetPreferences(Character character, IEnumerable<CharacterPreference> repository)
    {
        var preferences = new CharacterPreferences();

        var likes = repository.Where(cp => cp.CharacterId == character.CharacterId && cp.IsLike).Select(cp => cp.PreferenceId);
        preferences.Likes = likes;
        var dislikes = repository.Where(cp => cp.CharacterId == character.CharacterId && !cp.IsLike).Select(cp => cp.PreferenceId);
        preferences.Dislikes = dislikes;

        return preferences;
    }
}