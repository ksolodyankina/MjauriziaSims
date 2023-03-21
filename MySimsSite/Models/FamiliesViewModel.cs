using Domain.Entities;

namespace MjauriziaSims.Models
{
    public class FamiliesViewModel
    {
        public List<FamiliesWithUser> FamiliesWithUsers { get; set; }
        public Dictionary<int, bool> EditRules { get; set; }
        public MessageManager.MessageManager MsgManager { get; set; }
        public bool MyFamilies { get; set; } = false;
    }
}