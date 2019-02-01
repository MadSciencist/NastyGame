using System;

namespace Api.Identity.Models.DTOs
{
    public class UserDto
    {
        /// <summary>
        /// This is DTO class for retuning user info without sensitive data
        /// </summary>

        public UserDto(UserEntity user)
        {
            UserId = user.UserId;
            Login = user.Login;
            Name = user.Name;
            Email = user.Email;
            LastName = user.LastName;
            BirthDate = user.BirthDate;
            JoinDate = user.JoinDate;
        }

        public int UserId { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? JoinDate { get; set; }
    }
}
