using CryptoNews.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CryptoNews.WebAPI.Auth
{
    public class JwtAuthManager : IJwtAuthManager
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly IRefreshTokenService _refreshTokenService;

        public JwtAuthManager(IConfiguration configuration,
                            IUserService userService,
                            IRefreshTokenService refreshTokenSvc)
        {
            _configuration = configuration;
            _userService = userService;
            _refreshTokenService = refreshTokenSvc;
        }


        public async Task<JwtAuthResult> GenerateTokens(string email, Claim[] claims)
        {
            var jwt = new JwtSecurityToken("CryptoNews",
                "CryptoNews",
                claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                    SecurityAlgorithms.HmacSha256Signature)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            var userId = _userService.GetUserByEmail(email).Id;

            var principal = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims));
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(principal, userId);

            return new JwtAuthResult()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
