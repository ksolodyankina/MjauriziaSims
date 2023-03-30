using Domain.Entities;

namespace MjauriziaSims.Models
{
    public class RandomizerViewModel
    {
        public MessageManager.MessageManager MsgManager { get; set; }
        public IEnumerable<Goal> Goals { get; set; }
        public IEnumerable<Preference> Preferences { get; set; }
        public IEnumerable<Career> Careers { get; set; }
        public IEnumerable<Pack> Packs { get; set; }

        public Dictionary<int, IEnumerable<Pack>> GetPacksForFilter()
        {
            var packsForFilter = new Dictionary<int, IEnumerable<Pack>>();
            var categories = new string[5] { "base", "EP", "GP", "SP", "FP" };
            for (var i = 0; i < categories.Length; i++)
            {
                var packs = Packs.Where(p => p.Code.Contains(categories[i])).ToList();
                if (packs.Any())
                {
                    packsForFilter[i] = packs;
                }
            }

            return packsForFilter;
        }
    }
}