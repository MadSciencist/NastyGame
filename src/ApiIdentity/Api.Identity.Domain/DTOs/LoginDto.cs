using System.ComponentModel.DataAnnotations;

namespace Api.Identity.Domain.DTOs
{
    public class LoginDto
    {
        /// <summary>
        /// This is bind-model class for user login
        /// </summary>

        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Login { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(255)]
        public string Password { get; set; }
    }
}
