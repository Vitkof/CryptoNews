using CryptoNews.Core.IServices;
using CryptoNews.WebAPI.Auth;
using CryptoNews.WebAPI.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : Controller
    {
        private readonly IRefreshTokenService _tokenService;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IUserService _userService;


        public TokenController(IRefreshTokenService tokenSvc,
                               IJwtAuthManager authManager,
                               IConfiguration config,
                               IUserService userSvc)
        {
            _tokenService = tokenSvc ?? throw new ArgumentNullException(nameof(tokenSvc));
            _jwtAuthManager = authManager ?? throw new ArgumentNullException(nameof(authManager));
            _userService = userSvc ?? throw new ArgumentNullException(nameof(userSvc));
        }


        /// <summary>
        /// refresh token
        /// </summary>
        /// <param name="request">object includes refresh token string value</param>
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var user = await _userService.GetUserByRefreshToken(request.Token);

            if (user != null && !string.IsNullOrEmpty(user.Email))
            {
                if (!await _tokenService.IsRefreshTokenIsValidAsync(request.Token, user.Id))
                {
                    return BadRequest("Invalid Refresh Token");
                }

                var jwt = await _jwtAuthManager.GetJwt(user.Email);

                return Ok(jwt);
            }

            return BadRequest("Email or password is incorrect");
        }

        /// <summary>
        /// revoke token
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Revoke(Guid id)
        {
            await _tokenService.RevokeRefreshTokenAsync(id);
            return Ok("Token revoked");
        }
    }
}
