using CryptoNews.Core.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.Models.ViewModels
{
    public class NewsDetailsVM
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        [DisplayFormat(DataFormatString = "{0:MMM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime PubDate { get; set; }

        public float Rating { get; set; }
        public string Url { get; set; }

        public Guid? RssSourceId { get; set; }
        public string RssSourceName { get; set; }

        public IEnumerable<CommentWithInfoDto> Comments { get; set; }
    }
}
