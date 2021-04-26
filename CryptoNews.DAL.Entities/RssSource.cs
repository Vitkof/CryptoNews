using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CryptoNews.DAL.Entities
{
    public class RssSource
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        [Url]
        public string Url { get; set; }

        public virtual ICollection<News> NewsCollection { get; set; }
    }
}
