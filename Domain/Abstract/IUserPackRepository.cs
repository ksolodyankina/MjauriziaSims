using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Abstract
{
    public interface IUserPackRepository
    {
        IEnumerable<UserPack> UserPacks { get; }
        void SaveUserPack(UserPack userPack);
        void SaveUserPacks(int user, IEnumerable<int>? packs);
    }
}
