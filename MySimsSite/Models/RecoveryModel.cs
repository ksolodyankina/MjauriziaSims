using System.ComponentModel.DataAnnotations;

namespace MjauriziaSims.Models
{
    public class RecoveryModel
    {
        [Required(ErrorMessage = "Please, fill the email field")]
        [DataType(DataType.EmailAddress,ErrorMessage = "Please, input correct email")]
        public string Email { get; set; }
    }
}
