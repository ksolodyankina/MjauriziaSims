using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MjauriziaSims.Models
{
    public class CharacterViewModel
    {
        public Family Family { get; set; }
        public CharacterFormModel Character { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
        public IEnumerable<Career> Careers { get; set; }
        public MessageManager.MessageManager MsgManager { get; set; }
        public bool CanEdit { get; set; }
        public IEnumerable<Character> Characters { get; set; }

        public List<SelectListItem> GetSelectGoals()
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = MsgManager.Msg("notSet"), Value = "0" });

            foreach (var goal in Goals)
            {
                items.Add(new SelectListItem {
                    Text = MsgManager.Msg(goal.Title), 
                    Value = goal.GoalId.ToString()
                });
            }
            return items;
        }

        public string GetGoalsJSON()
        {
            var goals = new Dictionary<string, int[]> {
                { "child", new int[Goals.Where(g => g.IsChild).Count()]},
                { "adult", new int[Goals.Where(g => !g.IsChild).Count()]}
            };
            var i = 0;
            var j = 0;
            foreach (var goal in Goals)
            {
                if (goal.IsChild)
                {
                    goals["child"][i] = goal.GoalId;
                    i++;
                }
                else
                {
                    goals["adult"][j] = goal.GoalId;
                    j++;
                }
            }
            return Newtonsoft.Json.JsonConvert.SerializeObject(goals);
        }

        public List<SelectListItem> GetSelectPreferences()
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = MsgManager.Msg("notSet"), Value = "0" });

            foreach (var preference in Preferences)
            {
                items.Add(new SelectListItem { Text = MsgManager.Msg(preference.Title), Value = preference.PreferenceId.ToString() });
            }
            return items;
        }

        public List<SelectListItem> GetSelectCareer()
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = MsgManager.Msg("notSet"), Value = "0" });

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
            items.Add(new SelectListItem { Text = MsgManager.Msg("notSet"), Value = "-1" });

            for (var i = 0; i <= 1; i++)
            {
                items.Add(new SelectListItem 
                    { Text = MsgManager.Msg("chronotype_" + ((Chronotypes)(i)).ToString()), Value = $"{i}" });
            }
            return items;
        }

        public List<SelectListItem> GetParentSelectItems()
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = MsgManager.Msg("notSet"), Value = "0" });

            foreach (var character in Characters)
            {
                items.Add(new SelectListItem { Text = character.Name, Value = character.CharacterId.ToString() });
            }
            return items;
        }

        public List<SelectListItem> GetPartnerSelectItems()
        {
            var relatives = new List<Character>();

            var parents = Characters.Where(c => c.CharacterId == Character.Parent1 || c.CharacterId == Character.Parent2).ToList();                                     
            foreach (var parent in parents)
            {
                var grandParents = Characters.Where(c => c.CharacterId == parent.Parent1 || c.CharacterId == parent.Parent2).ToList();
                foreach (var grandParent in grandParents)
                {
                    relatives.Add(grandParent);
                }
            }

            for (var i = 0; i < 4; i++)
            {
                var rels = new List<Character>();
                foreach (var relative in relatives)
                {
                    var children = Characters.
                            Where(c => c.Parent1 == relative.CharacterId || c.Parent2 == relative.CharacterId)
                            .ToList();
                    foreach (var child in children)
                    {
                        if (!relatives.Contains(child))
                        {
                            rels.Add(child);
                        }
                    }
                }
                foreach (var rel in rels)
                {
                    relatives.Add(rel);
                }
                switch (i) 
                {
                    case 0:
                        {
                            foreach (var parent in parents)
                            {
                                if (!relatives.Contains(parent))
                                {
                                    relatives.Add(parent);
                                }
                            }
                        }
                    break;
                    case 2:
                        {
                            var children = Characters.
                                    Where(c => c.Parent1 == Character.CharacterId || c.Parent2 == Character.CharacterId).
                                    ToList();
                            foreach (var child in children)
                            {
                                if (!relatives.Contains(child))
                                {
                                    relatives.Add(child);
                                }
                            }
                        }
                    break;
                }
            }

            var possiblePartners = Characters.Except(relatives);
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = MsgManager.Msg("notSet"), Value = "0" });
            foreach (var ch in possiblePartners)
            {
                items.Add(new SelectListItem { Text = ch.Name, Value = ch.CharacterId.ToString() });
            }

            return items;
        }

        public List<SelectListItem> GetGenerationSelectItems()
        {
            var items = new List<SelectListItem>();

            for (var i = Family.Generation; i >= 1; i--)
            {
                items.Add(new SelectListItem { Text = i.ToString(), Value = i.ToString() });
            }
            return items;
        }

        public string GetMinAgeForPreferenceCategory(PreferenceCategories category)
        {
            var age = Preferences.Where(p => p.Category == category).Min(p => p.MinAge);

            return age.ToString().ToLower();
        }
    }
}