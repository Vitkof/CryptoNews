using CryptoNews.Core.DTO;
using System.Collections.Generic;
using MediatR;

namespace CryptoNews.DAL.CQS.Commands
{
    public class AddRangeNewsCommand : IRequest<int>
    {
        public IEnumerable<NewsDto> News { get; set; }
    }
}
