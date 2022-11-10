using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class Goal
    {
        [Display(Name = "ID")]
        public int GoalId { get; set; }

        [Display(Name = "Title")] 
        public string Title { get; set; } = "";

        [Display(Name = "IsChild")] 
        public bool IsChild { get; set; } = false;
    }
}
