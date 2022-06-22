using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CryptoNews.DAL.Entities
{
    public class User : BaseEntity<Guid>
    {
        [StringLength(50, MinimumLength =2), Required]
        public string FirstName { get; set; }

        [StringLength(50, MinimumLength = 2), Required]        
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        [StringLength(200)]
        public string ShortDescription { get; set; }

        [Url]
        public string AvatarUrl { get; set; }

        public GenderType Gender { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime RegisterTime { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public DateTimeOffset? LastActivityDate { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        public string PasswordHash { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<Comment> CommentsCollection { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokensCollection { get; set; }
    }


    public enum GenderType
    {
        [Display(Name = "M")]
        Male = 1,

        [Display(Name = "F")]
        Female = 2,

        [Display(Name = "T")]
        Trans = 3
    }
}
