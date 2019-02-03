using System;
using System.Collections.Generic;

namespace Api.Identity.Domain
{
    public class UserEntity
    {
        /// <summary>
        /// This is model class for mapping SQL => object
        /// </summary>
        public int UserId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public DateTime? JoinDate { get; set; }
        public IList<AddressEntity> Addresses { get; set; }
    }
}
