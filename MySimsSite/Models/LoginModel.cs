using System.ComponentModel.DataAnnotations;

namespace MjauriziaSims.Models
{
    public class LoginModel
    {
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
