using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MjauriziaSims.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "err_usernameRequired")]
        public string Username { get; set; }
        
        [Required(ErrorMessage = "err_emailRequired")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "err_passRequired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "err_passConfirmation")]
        public string ConfirmPassword { get; set; }
        public Roles Role { get; set; }
        public IEnumerable<int>? Packs { get; set; }
        public IEnumerable<Pack>? PackRepository { get; set; }
        public MessageManager.MessageManager? MsgManager { get; set; }

        public List<SelectListItem> GetPacksSelectItems()
        {
            var items = new List<SelectListItem>();

            foreach (var pack in PackRepository)
            {
                items.Add(new SelectListItem { Text = pack.Code, Value = pack.PackId.ToString() });
            }
            return items;
        }

        public Dictionary<int, IEnumerable<Pack>> GetPacks()
        {
            var packsResult = new Dictionary<int, IEnumerable<Pack>>();
            var categories = new string[4] { "EP", "GP", "SP", "FP" };
            for (var i = 0; i < categories.Length; i++)
            {
                var packs = PackRepository.Where(p => p.Code.Contains(categories[i])).ToList();
                if (packs.Any())
                {
                    packsResult[i] = packs;
                }
            }

            return packsResult;
        }
    }
}
