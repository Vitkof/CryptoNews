using CryptoNews.Core.DTO;
using MediatR;
using System.Collections.Generic;

namespace CryptoNews.DAL.CQS.Queries.News
{
    public class GetNewsWithoutRatingQuery : IRequest<IEnumerable<NewsDto>>
    {
    }
}
