using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.Commands
{
    public class AddNewsCommand
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
        public float Rating { get; set; }
        public Guid RssSourceId { get; set; }
        public DateTime PubDate { get; set; }
    }
}
