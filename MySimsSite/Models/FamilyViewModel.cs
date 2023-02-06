using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Web;
using MjauriziaSims.MessageManager;

namespace WebUI.Models
{
    public class FamilyViewModel
    {
        public FamiliesWithUser Family { get; set; }
        public IEnumerable<Character> Characters { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
        public IEnumerable<Career> Careers { get; set; }
        public IEnumerable<InheritanceLaw> InheritanceLaws { get; set; }
        public bool CanEdit { get; set; }
        public MessageManager MsgManager { get; set; }

        public bool CanGetMarried(Character character)
        {
            return character.IsHeir
                && !Characters.Any(c => c.Family == character.Family && c.Generation == character.Generation && c.InLow);
        }
        
        public bool CanBecomeHeir(Character character)
        {
            if (character.Age >= Ages.Young && !character.InLow)
            {
                return Characters.Any(c => c.IsHeir && c.Generation < character.Generation);
            }
            return false;
        }

        public string getCardColor(Character character)
        {
            return character.InFamily ? character.InLow ? "warning" : character.IsHeir ? "success" : "dark" : "secondary";
        }
    }
}