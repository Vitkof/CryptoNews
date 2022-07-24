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
        private readonly IRoleService _roleService;

        public JwtAuthManager(IConfiguration configuration,
                            IUserService userService,
                            IRoleService roleService,
                            IRefreshTokenService refreshTokenSvc)
        {
            _configuration = configuration;
            _userService = userService;
            _roleService = roleService;
            _refreshTokenService = refreshTokenSvc;
        }

        /// <summary>
        /// Get JWT Token after successful login.
        /// </summary>
        /// /// <param name="email">string user's email-address</param>
        public async Task<JwtAuthResult> GetJwt(string email)
        {
            var roleName = _roleService.GetRoleNameByEmail(email);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, roleName)
            };

            var jwtResult = await GenerateTokens(email, claims);
            return jwtResult;
        }

        /// <summary>
        /// Get Principal.
        /// </summary>
        public ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])),
                ValidateLifetime = false
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            var jwtSecurityToken = (JwtSecurityToken)securityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256, StringComparison.OrdinalIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        /// <summary>
        /// Generate Access & Refresh Tokens.
        /// </summary>
        private async Task<JwtAuthResult> GenerateTokens(string email, Claim[] claims)
        {
            var secretKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var jwt = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(_configuration.GetValue<double>("Jwt:ExpiresMinutes")),
                signingCredentials: new SigningCredentials(
                    secretKey,
                    SecurityAlgorithms.HmacSha256Signature)
                );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);
            var userId = _userService.GetUserByEmail(email).Id;

            var principal = new ClaimsPrincipal(new ClaimsIdentity(jwt.Claims));
            var refreshToken = await _refreshTokenService.GenerateRefreshTokenAsync(principal, userId);

            return new JwtAuthResult()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token,
                Email = email,
                UserId = userId
            };
        }
    }
}
