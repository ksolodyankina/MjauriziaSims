using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MjauriziaSims.Models
{
    public class CharacterFormModel
    {
        public int CharacterId { get; set; } = 0;
        public string Name { get; set; }
        public int Family { get; set; }
        public int Generation { get; set; }
        public bool Glasses { get; set; }
        public int Goal { get; set; }
        public Ages Age { get; set; }
        public bool InFamily { get; set; } = true;
        public Chronotypes? Chronotype { get; set; }
        public int? Career { get; set; }
        public int Parent1 { get; set; }
        public int Parent2 { get; set; }
        public int Partner { get; set; }
        public IEnumerable<int>? Likes { get; set; }
        public IEnumerable<int>? Dislikes { get; set; }

        public static CharacterFormModel ModelForCharacter(Character character, IEnumerable<CharacterPreference> repository)
        {
            var model = new CharacterFormModel
            {
                CharacterId = character.CharacterId,
                Name = character.Name,
                Family = character.Family,
                Generation = character.Generation,
                Glasses = character.Glasses,
                Goal = character.Goal,
                Age = character.Age,
                InFamily = character.InFamily,
                Chronotype = character.Chronotype,
                Career = character.Career,
                Parent1 = character.Parent1,
                Parent2 = character.Parent2,
                Partner = character.Partner,
                Likes = repository.Where(cp => cp.CharacterId == character.CharacterId && cp.IsLike).Select(cp => cp.PreferenceId),
                Dislikes = repository.Where(cp => cp.CharacterId == character.CharacterId && !cp.IsLike).Select(cp => cp.PreferenceId)
            };

            return model;
        }

        public void SaveCharacter(ICharacterRepository characterRepository, ICharacterPreferenceRepository characterPreferenceRepository)
        {
            var character = new Character
            {
                CharacterId = CharacterId,
                Name = Name,
                Family = Family,
                Generation = Generation,
                Glasses = Glasses,
                Goal = Goal,
                Age = Age,
                InFamily = InFamily,
                Chronotype = Chronotype,
                Career = Career,
                Parent1 = Parent1,
                Parent2 = Parent2,
                Partner = Partner
            };

            characterRepository.SaveCharacter(character);
            if (CharacterId == 0)
            {
                CharacterId = characterRepository.Characters.
                    Last(c => c.Name == Name && c.Family == Family && c.Generation == Generation).CharacterId;
            }

            var count = (Likes == null ? 0 : Likes.Count()) + (Dislikes == null ? 0 : Dislikes.Count());
            if (count > 0)
            {
                var characterPreferences = new CharacterPreference[count];
                var i = 0;
                if (Likes != null)
                {
                    foreach (var preference in Likes)
                    {
                        var characterPreference = new CharacterPreference
                        {
                            CharacterId = CharacterId,
                            PreferenceId = preference,
                            IsLike = true
                        };
                        characterPreferences[i] = characterPreference;
                        i++;
                    }
                }
                if (Dislikes != null)
                {
                    foreach (var preference in Dislikes)
                    {
                        var characterPreference = new CharacterPreference
                        {
                            CharacterId = CharacterId,
                            PreferenceId = preference,
                            IsLike = false
                        };
                        characterPreferences[i] = characterPreference;
                        i++;
                    }
                }
                characterPreferenceRepository.SaveCharacterPreferences(characterPreferences);
            }
        }
    }
}