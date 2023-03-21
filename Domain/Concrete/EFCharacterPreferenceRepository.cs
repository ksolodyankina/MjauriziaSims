using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFCharacterPreferenceRepository : ICharacterPreferenceRepository
    {
        EFDbContext context;

        public EFCharacterPreferenceRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<CharacterPreference> CharacterPreferences
        {
            get { return context.CharacterPreferences; }
        }

        public void SaveCharacterPreference(CharacterPreference characterPreference)
        {
            var dbEntry = context.CharacterPreferences.FirstOrDefault(
                    c => c.CharacterId == characterPreference.CharacterId && c.PreferenceId == characterPreference.PreferenceId);
            if (dbEntry == null)
            {
                context.CharacterPreferences.Add(characterPreference);
            }
            else
            {
                dbEntry.IsLike = characterPreference.IsLike;
            }
            context.SaveChanges();
        }

        public void SaveCharacterPreferences(CharacterPreference[] characterPreferences)
        {
            var character = characterPreferences.First().CharacterId;
            var dbEntries = context.CharacterPreferences.Where(c => c.CharacterId == character);
            foreach (var dbEntry in dbEntries)
            {
                context.Remove(dbEntry);
            }
            foreach (var characterPreference in characterPreferences)
            {
                context.Add(characterPreference);
            }
            context.SaveChanges();
        }
    }
}
