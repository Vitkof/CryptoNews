using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CryptoNews.DAL.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }
        public string Author { get; set; }
        public string Text { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime CreateAt { get; set; }
        public bool Active { get; set; }

        public Guid NewId { get; set; }
        public virtual New New { get; set; }

        public Guid UserId { get; set; }
        public virtual User User { get; set; }
    }
}
