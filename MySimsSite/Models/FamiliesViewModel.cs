using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using MjauriziaSims.MessageManager;

namespace WebUI.Models
{
    public class FamiliesViewModel
    {
        public List<FamiliesWithUser> FamiliesWithUsers { get; set; }
        public List<InheritanceLaw> InheritanceLaws { get; set; }
        public Dictionary<int, bool> EditRules { get; set; }
        public MessageManager MsgManager { get; set; }
        public bool MyFamilies { get; set; } = false;
    }
}