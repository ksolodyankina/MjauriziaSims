using Domain.Entities;
using Domain.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Abstract;
using MjauriziaSims.MessageManager;

namespace WebUI.Models
{
    public class RandomizerViewModel
    {
        public MessageManager MsgManager { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
        public IEnumerable<Career> Careers { get; set; }
    }
}