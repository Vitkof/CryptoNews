using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CryptoNews.DAL.Entities
{
    public class Comment : IBaseEntity
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreateAt { get; set; }
        public bool Active { get; set; }

        public Guid NewsId { get; set; }
        public virtual News News { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
