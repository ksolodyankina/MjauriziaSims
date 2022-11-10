using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IGoalRepository
    {
        IEnumerable<Goal> Goals { get; }
        void SaveGoal(Goal goal);
    }
}
