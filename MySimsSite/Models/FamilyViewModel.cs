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
            return character.Partner == 0 && character.Age >= Ages.Young;
        }
        
        public string getCardColor(Character character)
        {
            return character.InFamily ? "dark" : "secondary";
        }
    }
}