using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface ICharacterRepository
    {
        IEnumerable<Character> Characters { get; }
        void SaveCharacter(Character character);
        void DeleteCharacter(Character character);
    }
}
