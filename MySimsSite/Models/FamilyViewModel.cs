using Domain.Entities;

namespace MjauriziaSims.Models
{
    public class FamilyViewModel
    {
        public FamiliesWithUser Family { get; set; }
        public IEnumerable<Character> Characters { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
        public IEnumerable<Career> Careers { get; set; }
        public bool CanEdit { get; set; }
        public MessageManager.MessageManager MsgManager { get; set; }

        public bool CanGetMarried(Character character)
        {
            return character.Partner == 0 && character.Age >= Ages.Young;
        }
        
        public string getCardColor(Character character)
        {
            return character.InFamily ? "dark" : "secondary";
        }
    }
}