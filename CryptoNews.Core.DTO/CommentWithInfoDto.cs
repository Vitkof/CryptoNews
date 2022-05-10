using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.Core.DTO
{
    public class CommentWithInfoDto : Dto
    {
        [StringLength(1000)]
        public string Text { get; set; }

        public DateTime CreateAt { get; set; }
        public bool Active { get; set; }
        public ushort Rating { get; set; }

        public Guid NewsId { get; set; }
        public Guid UserId { get; set; }
        public string UserFullName { get; set; }
        public string UserAvatarUrl { get; set; }
        public Guid? ParentId { get; set; }
    }
}
