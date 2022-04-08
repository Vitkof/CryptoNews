using CryptoNews.Core.DTO;
using MediatR;

namespace CryptoNews.DAL.CQS.Queries.RefreshToken
{
    public class GetRefreshTokenByTokenValueQuery : IRequest<RefreshTokenDto>
    {
        public string TokenValue { get; set; }
    }
}
