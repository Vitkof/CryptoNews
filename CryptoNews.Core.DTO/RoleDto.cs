using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.Core.DTO
{
    public class RoleDto
    {
        public Guid Id { get; set; }

        [StringLength(50, MinimumLength = 2), Required]
        [RegularExpression(@"^([A-Z]+|[А-ЯЁ]+)([a-zA-Z]*|[а-яёА-ЯЁ]*)$")]
        public string Name { get; set; }

    }
}
