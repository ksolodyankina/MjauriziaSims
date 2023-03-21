using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFPreferenceRepository : IPreferenceRepository
    {
        EFDbContext context;

        public EFPreferenceRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Preference> Preferences
        {
            get { return context.Preferences; }
        }


        public void SavePreference(Preference preference)
        {
            if (preference.PreferenceId == 0)
            {
                context.Preferences.Add(preference);
            }
            else
            {
                var dbEntry = context.Preferences.Find(preference.PreferenceId);
                if (dbEntry != null)
                {
                    dbEntry.Code = preference.Code;
                    dbEntry.Title = preference.Title;
                    dbEntry.Category = preference.Category;
                    dbEntry.MinAge = preference.MinAge;
                }
            }
            context.SaveChanges();
        }
    }
}
