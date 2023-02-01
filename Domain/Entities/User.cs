﻿using System.ComponentModel.DataAnnotations;
namespace Domain.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        public bool IsActive { get; set; } = false;
        public Guid ConfirmationToken { get; set; }
    }
}
