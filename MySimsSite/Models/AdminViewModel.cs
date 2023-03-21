using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MjauriziaSims.Models
{
    public class AdminViewModel
    {
        public static List<SelectListItem> GetAgeSelectItems()
        {
            var items = new List<SelectListItem>();

            for (var i = 0; i <= 6; i++)
            {
                items.Add(new SelectListItem { Text = ((Ages)i).ToString(), Value = i.ToString() });
            }
            return items;
        }
    }
}