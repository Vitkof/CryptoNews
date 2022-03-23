using CryptoNews.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.DAL.CQS.Queries.News
{
    public class GetNewsByIdQuery : IQuery<NewsDto>
    {
        public Guid Id { get; set; }

        public GetNewsByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
