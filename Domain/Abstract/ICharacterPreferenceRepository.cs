using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface ICharacterPreferenceRepository
    {
        IEnumerable<CharacterPreference> CharacterPreferences { get; }
        void SaveCharacterPreference(CharacterPreference characterPreference);
        void SaveCharacterPreferences(CharacterPreference[] characterPreferences);
    }
}
