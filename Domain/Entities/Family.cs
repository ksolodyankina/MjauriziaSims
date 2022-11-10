using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Family
    {
        enum ChallengeType
        {
            Legacy,
            Decades
        }
        public int FamilyId { get; set; }
        public string Surname { get; set; }
        public int Generation { get; set; } = 1;
        public int Challenge { get; set; } = 0;

        public static List<SelectListItem> GetChallengeSelectCategory()
        {
            var items = new List<SelectListItem>();

            for (var i = 0; i <= 1; i++)
            {
                items.Add(new SelectListItem { Text = ((ChallengeType)i).ToString(), Value = $"{i}" });
            }
            return items;
        }
    }
}
