using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CryptoNews.DAL.Entities
{
    public class Comment : IBaseEntity
    {
        public Guid Id { get; set; }
        [StringLength(1000)]
        public string Text { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreateAt { get; set; }
        public bool Active { get; set; }
        public ushort Rating { get; set; }

        public Guid NewsId { get; set; }
        public virtual News News { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }

        public Guid? ParentId { get; set; }
        public virtual Comment Parent { get; set; }
    }
}
