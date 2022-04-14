using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoNews.DAL.Entities;

namespace CryptoNews.Core.DTO
{
    public class UserDto : Dto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string ShortDescription { get; set; }

        public string AvatarUrl { get; set; }
        public GenderType Gender { get; set; }
        public DateTime RegisterTime { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public DateTimeOffset? LastActivityDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public Guid RoleId { get; set; }
    }
}
