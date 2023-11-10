using System.ComponentModel.DataAnnotations;

namespace MjauriziaSims.Models
{
    public class ChangePassModel
    {
        [Required(ErrorMessage = "err_passRequired")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "err_passRequired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "err_passConfirmation")]
        public string ConfirmPassword { get; set; }
    }
}
