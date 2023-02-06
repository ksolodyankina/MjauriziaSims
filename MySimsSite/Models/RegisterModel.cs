using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace MjauriziaSims.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "err_loginRequired")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "err_emailRequired")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "err_passRequired")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "err_passConfirmation")]
        public string ConfirmPassword { get; set; }
        public Roles Role { get; set; }
    }
}
