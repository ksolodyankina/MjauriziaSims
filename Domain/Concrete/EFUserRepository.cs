using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFUserRepository : IUserRepository
    {
        EFDbContext context;

        public EFUserRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }


        public void SaveUser(User user)
        {
            if (user.UserId == 0)
            {
                context.Users.Add(user);
            }
            else
            {
                User dbEntry = context.Users.Find(user.UserId);
                if (dbEntry != null)
                {
                    dbEntry.Login = user.Login;
                    dbEntry.Email = user.Email;
                    dbEntry.Password = user.Password;
                    dbEntry.ConfirmationToken = user.ConfirmationToken;
                    dbEntry.IsActive = user.IsActive;
                }
            }
            context.SaveChanges();
        }
    }
}
