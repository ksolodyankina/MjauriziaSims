using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    enum InheritanceCategory
    {
        Gender,
        Bloodline,
        Heir,
        Species
    }
    public class InheritanceLaw
    {
        [Key]
        public int InheritanceId { get; set; } = 0;
        public string Code { get; set; }
        public string Title { get; set; }
        public bool AllowsManualChoice { get; set; }
        public int Category { get; set; }
        public int Value { get; set; }
        public bool IsStrict { get; set; }



        public IEnumerable<Character> LawFilter(IEnumerable<Character> characters)
        {
            var result = characters;
            if (!AllowsManualChoice)
            {
                if (Value == -1)
                {

                }
                else
                {
                    switch (Category)
                    {
                        case 0:
                            result = characters.Where(c => (int)c.Gender == Value);
                            break;
                        case 1:
                            result = characters.Where(c => c.IsAdopted == (Value == 1));
                            break;
                        case 2:
                            var c = characters.OrderBy(c => c.CharacterId * (Value == 0 ? 1 : -1)).FirstOrDefault();
                            var id = -1;
                            if (c != null)
                            {
                                id = c.CharacterId;
                            }
                            result = characters.Where(c => c.CharacterId == id);
                            break;
                        case 3:
                            result = characters.Where(c => c.IsAlien == (Value == 1));
                            break;

                    }
                }
            }
            if (result.Any() || IsStrict)
            {
                return result;
            }
            else
            {
                return characters;
            }
        }
    }
}
