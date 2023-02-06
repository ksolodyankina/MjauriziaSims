using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Localization;

namespace MjauriziaSims.Models
{
    public class RecoveryModel
    {
        [Required(ErrorMessage = "err_required")]
        [DataType(DataType.EmailAddress,ErrorMessage = "err_DataType")]
        public string Email { get; set; }
    }
}
