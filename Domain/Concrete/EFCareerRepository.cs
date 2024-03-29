﻿using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFCareerRepository : ICareerRepository
    {
        EFDbContext context;

        public EFCareerRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Career> Careers
        {
            get { return context.Careers; }
        }


        public void SaveCareer(Career career)
        {
            if (career.CareerId == 0)
            {
                context.Careers.Add(career);
            }
            else
            {
                Career dbEntry = context.Careers.Find(career.CareerId);
                if (dbEntry != null)
                {
                    dbEntry.Code = career.Code;
                    dbEntry.Title = career.Title;
                    dbEntry.Pack = career.Pack;
                }
            }
            context.SaveChanges();
        }
    }
}
