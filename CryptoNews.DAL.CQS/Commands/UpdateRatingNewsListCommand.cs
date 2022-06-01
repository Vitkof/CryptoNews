using CryptoNews.Core.DTO;
using MediatR;
using System.Collections.Generic;

namespace CryptoNews.DAL.CQS.Commands
{
    public class UpdateRatingNewsListCommand : IRequest<int>
    {
        public IEnumerable<NewsDto> NewsDtos { get; set; }
    }
}
