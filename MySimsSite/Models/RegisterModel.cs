using System.ComponentModel.DataAnnotations;
using Domain.Entities;

namespace MjauriziaSims.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Please, fill the login field")]
        public string Login { get; set; }
        
        [Required(ErrorMessage = "Please, fill the email field")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please, fill the password field")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
        public Roles Role { get; set; }
    }
}
