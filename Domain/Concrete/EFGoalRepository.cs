using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFGoalRepository : IGoalRepository
    {
        EFDbContext context;

        public EFGoalRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<Goal> Goals
        {
            get { return context.Goals; }
        }


        public void SaveGoal(Goal goal)
        {
            if (goal.GoalId == 0)
            {
                context.Goals.Add(goal);
            }
            else
            {
                Goal dbEntry = context.Goals.Find(goal.GoalId);
                if (dbEntry != null)
                {
                    dbEntry.Code = goal.Code;
                    dbEntry.Title = goal.Title;
                    dbEntry.IsChild = goal.IsChild;
                    dbEntry.Pack = goal.Pack;
                }
            }
            context.SaveChanges();
        }
    }
}
