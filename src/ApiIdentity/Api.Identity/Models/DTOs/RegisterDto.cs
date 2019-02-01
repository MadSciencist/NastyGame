using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Identity.Models.DTOs
{
    public class RegisterDto
    {
        /// <summary>
        /// This is bind-model class for registering new user
        /// </summary>

        [Required]
        [MaxLength(255)]
        public string Login { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(255)]
        public string LastName { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
