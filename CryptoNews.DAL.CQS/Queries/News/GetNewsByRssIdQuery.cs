using CryptoNews.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.Queries.News
{
    public class GetNewsByRssIdQuery : IQuery<IEnumerable<NewsDto>>
    {
        public Guid RssId { get; set; }

        public GetNewsByRssIdQuery(Guid id)
        {
            RssId = id;
        }
    }
}
