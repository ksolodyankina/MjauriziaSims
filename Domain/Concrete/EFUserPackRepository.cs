using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete
{
    public class EFUserPackRepository : IUserPackRepository
    {
        EFDbContext context;

        public EFUserPackRepository(EFDbContext context)
        {
            this.context = context;
        }

        public IEnumerable<UserPack> UserPacks
        {
            get { return context.UserPacks; }
        }

        public void SaveUserPack(UserPack userPack)
        {
            var dbEntry = context.UserPacks.FirstOrDefault(
                    u => u.UserId == userPack.UserId && u.PackId == userPack.PackId);
            if (dbEntry == null)
            {
                context.UserPacks.Add(userPack);
            }
            context.SaveChanges();
        }

        public void SaveUserPacks(int user, IEnumerable<int>? packs)
        {
            var count = packs == null ? 0 : packs.Count();
            var userPacks = new UserPack[count];
            var i = 0;
            if (count > 0)
            {
                foreach (var pack in packs)
                {
                    var userPack = new UserPack
                    {
                        UserId = user,
                        PackId = pack
                    };
                    userPacks[i] = userPack;
                    i++;
                }
            }
            var dbEntries = context.UserPacks.Where(u => u.UserId == user);
            foreach (var dbEntry in dbEntries)
            {
                context.Remove(dbEntry);
            }
            foreach (var userPack in userPacks)
            {
                context.Add(userPack);
            }
            context.SaveChanges();
        }
    }
}
