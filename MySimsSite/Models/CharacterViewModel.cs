using Domain.Entities;
using Domain.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Abstract;
using MjauriziaSims.MessageManager;

namespace WebUI.Models
{
    public class CharacterViewModel
    {
        public Family Family { get; set; }
        public Character Character { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
        public IEnumerable<Career> Careers { get; set; }
        public MessageManager MsgManager { get; set; }

        public CharacterViewModel
                    (Family family, 
                    Character character, 
                    IEnumerable<Goal> goals, 
                    IEnumerable<Preference> preferences, 
                    IEnumerable<Career> careers,
                    MessageManager msgManager)
        {
            Family = family;
            Character = character;
            Goals = goals;
            Preferences = preferences;
            Careers = careers;
            MsgManager = msgManager;
        }
        
        public List<SelectListItem> GetSelectCategory(bool includeChildGoals = false)
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Not set", Value = "0" });

            foreach (var goal in Goals.Where(g => !g.IsChild || includeChildGoals ? true : false))
            {
                items.Add(new SelectListItem { Text = MsgManager.Msg(goal.Title), Value = goal.GoalId.ToString() });
            }
            return items;
        }

        public List<SelectListItem> GetSelectPreferences(int category)
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Not set", Value = "0" });

            foreach (var preference in Preferences.Where(p => p.Category == (Preference.Categories)category))
            {
                items.Add(new SelectListItem { Text = MsgManager.Msg(preference.Title), Value = preference.PreferenceId.ToString() });
            }
            return items;
        }

        public List<SelectListItem> GetSelectCareer()
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Not set", Value = "0" });

            foreach (var career in Careers)
            {
                items.Add(new SelectListItem { Text = MsgManager.Msg(career.Title), Value = career.CareerId.ToString() });
            }
            return items;
        }

        public List<SelectListItem> GetAgeSelectItems(bool allValues = false)
        {
            var items = new List<SelectListItem>();

            for (var i = allValues ? 0 : 4; i <= 6; i++)
            {
                items.Add(new SelectListItem { Text = MsgManager.Msg("age_" + ((Ages)i).ToString()), Value = $"{i}" });
            }
            return items;
        }
        
        public List<SelectListItem> GetChronotypeSelectItems()
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Not set", Value = "-1" });

            for (var i = 0; i <= 1; i++)
            {
                items.Add(new SelectListItem 
                    { Text = MsgManager.Msg("chronotype_" + ((Chronotypes)(i)).ToString()), Value = $"{i}" });
            }
            return items;
        }

        public List<SelectListItem> GetGenderSelectItems()
        {
            var items = new List<SelectListItem>();

            for (var i = 0; i <= 1; i++)
            {
                items.Add(new SelectListItem { Text = MsgManager.Msg("gender_" + ((Genders)(i)).ToString()), Value = $"{i}" });
            }
            return items;
        }
    }
}