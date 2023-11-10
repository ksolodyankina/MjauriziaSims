using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MjauriziaSims.Models
{
    public class UserEditModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
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
            var categories = new string[4] {"EP", "GP", "SP", "FP" };
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
