using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IPreferenceRepository
    {
        IEnumerable<Preference> Preferences { get; }
        void SavePreference(Preference preference);
    }
}
