using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CryptoNews.DAL.Entities
{
    public class User : IBaseEntity
    {
        public Guid Id { get; set; }

        [StringLength(50, MinimumLength =2), Required]
        [RegularExpression(@"^([A-Z]+|[А-ЯЁ]+)([a-zA-Z]*|[а-яёА-ЯЁ]*)$")]
        public string FirstName { get; set; }
        [StringLength(50, MinimumLength = 2), Required]
        [RegularExpression(@"^([A-Z]+|[А-ЯЁ]+)([a-zA-Z]*|[а-яёА-ЯЁ]*)$")]
        public string LastName { get; set; }

        public string FullName {
            get {
                return $"{FirstName} {LastName}";
            } 
        }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public virtual ICollection<Comment> CommentsCollection { get; set; }
    }
}
