using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    [PrimaryKey(nameof(CharacterId), nameof(PreferenceId))]
    public class CharacterPreference
    {
        public int CharacterId { get; set; }
        public int PreferenceId { get; set; }
        public bool IsLike { get; set; }
    }
}
