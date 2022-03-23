using CryptoNews.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.Queries.Rss
{
    public class GetRssByIdQuery : IQuery<RssSourceDto>
    {
        public Guid Id { get; set; }

        public GetRssByIdQuery(Guid id)
        {
            Id = id;
        }

    }
}
