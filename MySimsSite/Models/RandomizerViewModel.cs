using Domain.Entities;

namespace MjauriziaSims.Models
{
    public class RandomizerViewModel
    {
        public MessageManager.MessageManager MsgManager { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
        public IEnumerable<Career> Careers { get; set; }
    }
}