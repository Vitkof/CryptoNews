using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CryptoNews.DAL.Entities
{
    public class New
    {
        public Guid Id { get; set; }
        [StringLength(150, MinimumLength = 3)]
        [Required(ErrorMessage ="Title is required.")]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required(ErrorMessage ="Text is required.")]
        public string Body { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime PubDate { get; set; }
        
        public float Rating { get; set; }
        [Url]
        public string Url { get; set; }

        public Guid RssSourceId { get; set; } //ForeignKey
        public virtual RssSource RssSource { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
