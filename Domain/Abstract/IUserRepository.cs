using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IUserRepository
    {
        IEnumerable<User> Users { get; }
        void SaveUser(User user);
    }
}
