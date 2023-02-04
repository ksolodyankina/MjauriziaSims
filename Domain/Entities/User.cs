using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public enum Roles
    {
        User,
        Admin
    } 
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public bool IsActive { get; set; } = false;
        public Guid ConfirmationToken { get; set; }
        public Roles Role { get; set; } = Roles.User;
        
        public List<SelectListItem> GetSelectRoles()
        {
            var items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Not set", Value = "0" });

            foreach (var role in (Roles[])Enum.GetValues(typeof(Roles)))
            {
                items.Add(new SelectListItem { Text = role.ToString(), Value = ((int)(role)).ToString() });
            }
            return items;
        }
    }

}
