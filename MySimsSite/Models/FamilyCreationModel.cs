using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MjauriziaSims.Models
{
    public class FamilyCreationModel
    {
        public Family Family { get; set; }
        public MessageManager.MessageManager MsgManager { get; set; }
    }
}