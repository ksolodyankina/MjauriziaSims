using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFInheritanceLawRepository : IInheritanceLawRepository
    {
        EFDbContext context;

        public EFInheritanceLawRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<InheritanceLaw> InheritanceLaws
        {
            get { return context.InheritanceLaws; }
        }


        public void SaveInheritanceLaw(InheritanceLaw inheritanceLaw)
        {
            if (inheritanceLaw.InheritanceId == 0)
            {
                context.InheritanceLaws.Add(inheritanceLaw);
            }
            else
            {
                var dbEntry = context.InheritanceLaws.Find(inheritanceLaw.InheritanceId);
                if (dbEntry != null)
                {
                    dbEntry.Code = inheritanceLaw.Code;
                    dbEntry.Title = inheritanceLaw.Title;
                    dbEntry.AllowsManualChoice = inheritanceLaw.AllowsManualChoice;
                    dbEntry.Category = inheritanceLaw.Category;
                    dbEntry.Value = inheritanceLaw.Value;
                    dbEntry.IsStrict = inheritanceLaw.IsStrict;
                }
            }
            context.SaveChanges();
        }
    }
}
