using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [PrimaryKey(nameof(UserId), nameof(PackId))]
    public class UserPack
    {
        public int UserId { get; set; }
        public int PackId { get; set; }
    }
}
