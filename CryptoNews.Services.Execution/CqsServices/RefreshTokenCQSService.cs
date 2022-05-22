using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.DAL.CQS.Commands;
using CryptoNews.DAL.CQS.Queries.RefreshToken;
using MediatR;
using Serilog;
using System;
using System.Security.Claims;
using System.Security.Cryptography;
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
            RefreshTokenDto newRT;
            var rndNumber = new byte[32];
            using(var generator = new RNGCryptoServiceProvider())
            {
                generator.GetBytes(rndNumber);
                newRT = new RefreshTokenDto
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Token = rndNumber.ToString(),
                    CreationTime = DateTime.Now.ToLocalTime(),
                    ElapsesUtc = DateTime.Now.ToLocalTime().AddHours(1)
                };
            }
            

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
            Log.Debug("Start refresh token validation");

            var query = new GetRefreshTokenByTokenValueQuery()
            { TokenValue = token };
            var rt = await _mediator.Send(query);

            if (rt == null)
            {
                Log.Warning("Invalid refresh token");
                return false;
            }

            /////////////////////////////////////////////
            // check if refresh token has expired
            /////////////////////////////////////////////
            if (rt.ElapsesUtc >= DateTime.Now)
            {
                Log.Warning("Refresh token has expired.");
                return false;
            }

            /////////////////////////////////////////////
            // check if client belongs to requested refresh token
            /////////////////////////////////////////////
            if (userId != rt.UserId)
            {
                Log.Error("{0} tries to refresh token belonging to {1}", userId, rt.UserId);
                return false;
            }

            return true;
        }

        public Task<RefreshTokenDto> UpdateRefreshTokenAsync(string handle, RefreshTokenDto refreshToken, Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
