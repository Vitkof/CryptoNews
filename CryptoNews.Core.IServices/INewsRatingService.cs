using System.Collections.Generic;
using System.Threading.Tasks;
using CryptoNews.Core.DTO;

namespace CryptoNews.Core.IServices
{
    public interface INewsRatingService
    {
        Task<IEnumerable<NewsDto>> Rating(IEnumerable<NewsDto> newsWithoutRating);
    }
}
