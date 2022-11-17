using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebUI.Models
{
    public class FamiliesViewModel
    {
        public List<Family> Families { get; set; }
        public List<InheritanceLaw> InheritanceLaws { get; set; }

    }
}