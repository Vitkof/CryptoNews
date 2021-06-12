using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CryptoNews.DAL.Entities
{
    public class Role : IBaseEntity
    {
        public Guid Id { get; set; }

        [StringLength(50, MinimumLength =2), Required]
        [RegularExpression(@"^([A-Z]+|[А-ЯЁ]+)([a-zA-Z]*|[а-яёА-ЯЁ]*)$")]
        public string Name { get; set; }
        
        public virtual ICollection<User> Users { get; set; }
    }
}
