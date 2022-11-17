using System.ComponentModel.DataAnnotations;
using Domain.Abstract;
using Domain.Concrete;
using Random = System.Random;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Domain.Entities
{
    public enum Genders
    {
        Male,
        Female
    }
    public enum Ages
    {
        Baby,
        Toddler,
        Child,
        Teen,
        Young,
        Adult,
        Old
    }

    public enum Sexualities
    {
        Heterosexual,
        Bisexual,
        Homosexual
    }

    public enum Chronotypes
    {
        Sleepyhead,
        Morning_Person,
        Nigth_Person,
        Hiperproductive
    }

    public class Character
    {
        public int CharacterId { get; set; } = 0;
        public string Name { get; set; }
        public int Family { get; set; }
        public int Generation { get; set; } = 1;
        public bool Glasses { get; set; } = false;
        public int Goal { get; set; } = 0;
        public Ages Age { get; set; } = Ages.Baby;
        public bool IsHeir { get; set; } = false;
        public bool InLow { get; set; } = false;
        public bool InFamily { get; set; } = true;
        public int? Color { get; set; }
        public int? Music { get; set; }
        public int? Hobby { get; set; }
        public int? Decor { get; set; }
        public Sexualities? Sexuality { get; set; }
        public Chronotypes? Chronotype { get; set; }
        public Genders Gender { get; set; }
        public int? Career { get; set; }
        public bool IsAdopted { get; set; } = false;
        public bool IsAlien { get; set; } = false;
    }
}
