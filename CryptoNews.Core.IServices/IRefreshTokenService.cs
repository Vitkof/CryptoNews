using CryptoNews.Core.DTO;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CryptoNews.Core.IServices
{
    /// <summary>
    /// Implements refresh token creation and validation
    /// </summary>
    public interface IRefreshTokenService
    {
        Task<bool> IsRefreshTokenIsValidAsync(string token, Guid userId);
        Task<RefreshTokenDto> GenerateRefreshTokenAsync(ClaimsPrincipal subject, Guid userId);
        Task<RefreshTokenDto> UpdateRefreshTokenAsync(string handle, RefreshTokenDto refreshToken, Guid userId);
        
    }
}
