using MediatR;
using System;

namespace CryptoNews.DAL.CQS.Commands
{
    public class RevokeRefreshTokenCommand : IRequest<int>
    {
        public Guid TokenId { get; set; }
    }
}
