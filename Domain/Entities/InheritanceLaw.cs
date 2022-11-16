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
        public int InheritanceId { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public bool AllowsManualChoice { get; set; }
        public int Category { get; set; }
        public int Value { get; set; }
        public bool IsStrict { get; set; }
    }
}
