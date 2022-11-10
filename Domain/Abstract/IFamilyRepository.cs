using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IFamilyRepository
    {
        IEnumerable<Family> Families { get; }
        void SaveFamily(Family family);
        void DeleteFamily(Family family);
    }
}
