using Domain.Entities;
using System.Collections.Generic;

namespace MjauriziaSims.Models
{
    public class FamilyViewModel
    {
        public FamiliesWithUser Family { get; set; }
        public IEnumerable<CharacterFormModel> Characters { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
        public IEnumerable<Career> Careers { get; set; }
        public bool CanEdit { get; set; }
        public MessageManager.MessageManager MsgManager { get; set; }

        public bool CanGetMarried(CharacterFormModel character)
        {
            return character.Partner == 0 && character.Age >= Ages.Young;
        }
        
        public string GetCardColor(CharacterFormModel character)
        {
            return character.InFamily ? "dark" : "secondary";
        }

        public List<Preference> GetCharacterPreferences(CharacterFormModel character, bool isLike)
        {
            var preferences = isLike ? character.Likes : character.Dislikes;
            var result = new List<Preference>();
            foreach (var preference in preferences)
            {
                result.Add(Preferences.First(p => p.PreferenceId == preference));                
            }

            return result;
        }

        public Dictionary<PreferenceCategories, Dictionary<string, List<Preference>>> GetCharacterPreferences2(CharacterFormModel character)
        {
            var result = new Dictionary<PreferenceCategories, Dictionary<string, List<Preference>>>();
            if (character.Likes != null)
            {
                foreach (var preference in character.Likes)
                {
                    var preferenceModel = Preferences.First(p => p.PreferenceId == preference);
                    if (!result.ContainsKey(preferenceModel.Category))
                    {
                        result[preferenceModel.Category] = new Dictionary<string, List<Preference>>();
                        result[preferenceModel.Category]["likes"] = new List<Preference>();
                    }
                    result[preferenceModel.Category]["likes"].Add(preferenceModel);
                }
            }
            if (character.Dislikes != null)
            {
                foreach (var preference in character.Dislikes)
                {
                    var preferenceModel = Preferences.First(p => p.PreferenceId == preference);
                    if (!result.ContainsKey(preferenceModel.Category))
                    {
                        result[preferenceModel.Category] = new Dictionary<string, List<Preference>>();
                    }
                    if (!result[preferenceModel.Category].ContainsKey("dislikes"))
                    {
                        result[preferenceModel.Category]["dislikes"] = new List<Preference>();
                    }
                    result[preferenceModel.Category]["dislikes"].Add(preferenceModel);
                }
            }

            return result;
        }
    }
}