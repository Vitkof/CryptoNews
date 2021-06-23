using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CryptoNews.DAL.Entities
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }

        [StringLength(50, MinimumLength =2), Required]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 2), Required]        
        public string LastName { get; set; }

        public string FullName {
            get {
                return $"{FirstName} {LastName}";
            } 
        }

        [DataType(DataType.DateTime)]
        public DateTime RegisterTime { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<Comment> CommentsCollection { get; set; }
    }
}
