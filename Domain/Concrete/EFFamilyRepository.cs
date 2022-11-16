using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFFamilyRepository : IFamilyRepository
    {
        EFDbContext context;

        public EFFamilyRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Family> Families
        {
            get { return context.Families; }
        }


        public void SaveFamily(Family family)
        {
            if (family.FamilyId == 0)
            {
                context.Families.Add(family);
            }
            else
            {
                Family dbEntry = context.Families.Find(family.FamilyId);
                if (dbEntry != null)
                {
                    dbEntry.Surname = family.Surname;
                    dbEntry.Generation = family.Generation;
                    dbEntry.Inheritance1 = family.Inheritance1;
                    dbEntry.Inheritance2 = family.Inheritance2;
                    dbEntry.Inheritance3 = family.Inheritance3;
                    dbEntry.Inheritance4 = family.Inheritance4;
                }
            }
            context.SaveChanges();
        }

        public void DeleteFamily(Family family)
        {
            foreach (var character in context.Characters.Where(c => c.Family == family.FamilyId))
            {
                context.Characters.Remove(character);
            }
            context.Families.Remove(family);
            context.SaveChanges();
        }
    }
}
