using Domain.Entities;
using Domain.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using Domain.Abstract;

namespace WebUI.Models
{
    public class CharacterViewModel
    {
        public Family Family { get; set; }
        public Character Character { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
        public IEnumerable<Career> Careers { get; set; }

        public CharacterViewModel
                    (Family family, 
                    Character character, 
                    IEnumerable<Goal> goals, 
                    IEnumerable<Preference> preferences, 
                    IEnumerable<Career> careers)
        {
            Family = family;
            Character = character;
            Goals = goals;
            Preferences = preferences;
            Careers = careers;
        }


        public List<SelectListItem> GetSelectCategory(bool includeChildGoals = false)
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Not set", Value = "0" });

            foreach (var goal in Goals.Where(g => !g.IsChild || includeChildGoals ? true : false))
            {
                items.Add(new SelectListItem { Text = goal.Title, Value = goal.GoalId.ToString() });
            }
            return items;
        }

        public List<SelectListItem> GetSelectPreferences(int category)
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Not set", Value = "0" });

            foreach (var preference in Preferences.Where(p => p.Category == (Preference.Categories)category))
            {
                items.Add(new SelectListItem { Text = preference.Title, Value = preference.PreferenceId.ToString() });
            }
            return items;
        }

        public List<SelectListItem> GetSelectCareer(int generation)
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Not set", Value = "0" });

            foreach (var career in Careers)
            {
                items.Add(new SelectListItem { Text = career.Title, Value = career.CareerId.ToString() });
            }
            return items;
        }

        public List<SelectListItem> GetSelectItems(int start, int end, Func<int, Enum> f, bool addNotSetItem = true)
        {
            var items = new List<SelectListItem>();
            if (addNotSetItem)
            {
                items.Add(new SelectListItem { Text = "Not set", Value = "-1" });
            }

            for (var i = start; i <= end; i++)
            {
                items.Add(new SelectListItem { Text = f(i).ToString(), Value = $"{i}" });
            }
            return items;
        }

        public List<SelectListItem> GetAgeSelectItems(bool allValues = false)
        {
            return GetSelectItems(allValues ? 0 : 4, 6, i => (Ages)i, false);
        }

        public List<SelectListItem> GetSexualitySelectItems()
        {
            return GetSelectItems(0, 2, i => (Sexualities)i, false);
        }

        public List<SelectListItem> GetChronotypeSelectItems()
        {
            return GetSelectItems(0, 3, i => (Chronotypes)i);
        }

        public List<SelectListItem> GetGenderSelectItems()
        {
            return GetSelectItems(0, 1, i => (Genders)i, false);
        }
    }
}