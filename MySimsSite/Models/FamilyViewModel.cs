using Domain.Entities;
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

        public int GetNextHeirId()
        {
            var result = -1;
            if (Family.Challenge == 0)
            {
                var nextHeir = Characters.First(c => c.Family == Family.FamilyId && c.Generation == Family.Generation);
                var isMarried = Characters
                    .Any(c => c.Family == nextHeir.Family && c.Generation == nextHeir.Generation && c.InLow);

                result = nextHeir.CharacterId;
                if ((nextHeir.IsHeir && nextHeir.Generation != 1) || isMarried)
                {
                    result = -1;
                }
            } else if (Family.Challenge == 1)
            {
                var currentHeir = Characters.Last(c => c.Family == Family.FamilyId && c.IsHeir);
                var children = Characters.Where(c => c.Family == Family.FamilyId && c.Generation == currentHeir.Generation + 1);
                if (children.Count() > 0)
                {
                    var nextHeir = children.LastOrDefault(c => c.Gender == 0);
                    if (nextHeir == null)
                    {
                        nextHeir = children.Last();
                    }

                    result = nextHeir.CharacterId;
                }
            }

            return result;
        }

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
            return character.InFamily ? character.InLow ? "warning" : character.IsHeir ? "success" : "primary" : "secondary";
        }
    }
}