using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; } = false;
        public Guid ConfirmationToken { get; set; }
    }
}
