using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFPackRepository : IPackRepository
    {
        EFDbContext context;

        public EFPackRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Pack> Packs
        {
            get { return context.Packs; }
        }


        public void SavePack(Pack pack)
        {
            if (pack.PackId == 0)
            {
                context.Packs.Add(pack);
            }
            else
            {
                Pack dbEntry = context.Packs.Find(pack.PackId);
                if (dbEntry != null)
                {
                    dbEntry.Code = pack.Code;
                    dbEntry.Title = pack.Title;
                }
            }
            context.SaveChanges();
        }
    }
}
