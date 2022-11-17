using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public enum InheritanceCategory
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
        public InheritanceCategory Category { get; set; }
        public int Value { get; set; }
        public bool IsStrict { get; set; }

    }
}
