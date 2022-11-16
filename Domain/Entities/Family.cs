using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Family
    {
        public int FamilyId { get; set; }
        public string Surname { get; set; }
        public int Generation { get; set; } = 1;
        public List<InheritanceLaw> InheritanceLaw { get; set; } = new List<InheritanceLaw>();

    }
}
