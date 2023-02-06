using System.ComponentModel.DataAnnotations;

namespace MjauriziaSims.Models
{
    public class ResetPassModel
    {
        [Required]
        public int UserId { get; set; }
        
        [Required]
        public Guid ConfirmationToken { get; set; }

        [Required(ErrorMessage = "err_passRequired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "err_passConfirmation")]
        public string ConfirmPassword { get; set; }
    }
}
