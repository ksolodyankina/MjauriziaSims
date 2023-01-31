using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class Family
    {
        public int FamilyId { get; set; }
        public int UserId { get; set; }
        public string Surname { get; set; }
        public int Generation { get; set; } = 1;
        public int Inheritance1 { get; set; }
        public int Inheritance2 { get; set; }
        public int Inheritance3 { get; set; }
        public int Inheritance4 { get; set; }

    }
}
