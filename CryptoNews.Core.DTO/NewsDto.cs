using System;
using System.ComponentModel.DataAnnotations;

namespace CryptoNews.Core.DTO
{
    public class NewsDto : Dto
    {
        [StringLength(150, MinimumLength = 3), Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public string Body { get; set; }

        [DisplayFormat(DataFormatString = "{0:MMM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime PubDate { get; set; }

        public float Rating { get; set; }
        [Url]
        public string Url { get; set; }

        public Guid? RssSourceId { get; set; }
    }
}
