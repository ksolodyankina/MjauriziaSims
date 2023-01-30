using System.ComponentModel.DataAnnotations;

namespace MjauriziaSims.Models
{
    public class ResetPassModel
    {
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public Guid ConfirmationToken { get; set; }

        [Required(ErrorMessage = "Please, fill the password field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
