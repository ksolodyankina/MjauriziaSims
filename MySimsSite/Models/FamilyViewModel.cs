using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
using System.Web;

namespace WebUI.Models
{
    public class FamilyViewModel
    {
        public Family Family { get; set; }
        public IEnumerable<Character> Characters { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
        public IEnumerable<Career> Careers { get; set; }
        public IEnumerable<InheritanceLaw> InheritanceLaws { get; set; }

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

        public List<SelectListItem> GetInheritanceSelectCategory(int category)
        {
            var items = new List<SelectListItem>();

            foreach (var law in InheritanceLaws.Where(l => (int)l.Category == category))
            {
                items.Add(new SelectListItem { Text = law.Title, Value = law.InheritanceId.ToString() });
            }
            return items;
        }


    }
}