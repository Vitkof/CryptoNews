using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.CQS.Queries.RefreshToken;
using MediatR;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CryptoNews.Services.Implement.CqsServices
{
    public class RefreshTokenCQSService : IRefreshTokenService
    {
        private readonly IMediator _mediator;

        public RefreshTokenCQSService(IMediator mediator)
        {
            _mediator = mediator;
        }


        public async Task<RefreshTokenDto> GenerateRefreshTokenAsync(ClaimsPrincipal subject, Guid userId)
        {
            var newRT = new RefreshTokenDto
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                Token = Guid.NewGuid().ToString(),
                CreationTime = DateTime.Now.ToLocalTime(),
                ElapsesUtc = DateTime.Now.ToLocalTime().AddHours(1)
            };

            var cmd = new UpdateRefreshTokenCommand()
            {
                UserId = userId,
                NewRefreshToken = newRT
            };
            await _mediator.Send(cmd);

            return newRT;
        }

        public async Task<bool> IsRefreshTokenIsValidAsync(string token, Guid userId)
        {
            var query = new GetRefreshTokenByTokenValueQuery()
            { TokenValue = token };
            var rt = await _mediator.Send(query);

            return rt != null &&
                   rt.ElapsesUtc >= DateTime.Now;
        }

        public async Task<RefreshTokenDto> UpdateRefreshTokenAsync(string handle, RefreshTokenDto refreshToken, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
