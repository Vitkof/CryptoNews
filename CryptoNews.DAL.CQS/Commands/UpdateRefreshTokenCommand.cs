using CryptoNews.Core.DTO;
using MediatR;
using System;

namespace CryptoNews.DAL.CQS.Commands
{
    public class UpdateRefreshTokenCommand : IRequest<int>
    {
        public Guid UserId { get; set; }
        public RefreshTokenDto NewRefreshToken { get; set; }
    }
}
