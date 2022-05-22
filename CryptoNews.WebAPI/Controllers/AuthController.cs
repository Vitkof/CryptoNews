using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.WebAPI.Auth;
using CryptoNews.WebAPI.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CryptoNews.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IJwtAuthManager _jwtAuthManager;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthController(IUserService userSvc,
                              IRoleService roleSvc,
                              IJwtAuthManager authManager,
                              IRefreshTokenService refreshTokenSvc)
        {
            _userService = userSvc;
            _roleService = roleSvc;
            _jwtAuthManager = authManager;
            _refreshTokenService = refreshTokenSvc;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                if(_userService.GetUserByEmail(request.Email) != null)
                {
                    return BadRequest("User with this email already existed");
                }

                var passwordHash = _userService.GetHashPassword(request.Password);

                var isRegistered = await _userService.AddUser(new UserDto
                {
                    Id = Guid.NewGuid(),
                    Email = request.Email,
                    PasswordHash = passwordHash
                });

                if (isRegistered)
                {
                    var jwt = await GetJwt(request.Email);
                    return Ok(jwt);
                }

                return BadRequest("Unsuccessful registration");
            }
            catch(Exception ex)
            {
                Log.Error(ex.Message);
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (_userService.GetUserByEmail(request.Email) == null)
            {
                return BadRequest("No user");
            }

            var passwordHash = _userService.GetHashPassword(request.Password);
            var isValid = await _userService.CheckAuthIsValid(new UserDto() 
            { Email = request.Email, PasswordHash = passwordHash });

            if (isValid)
            {
                var jwtAuthResult = await GetJwt(request.Email);
                return Ok(jwtAuthResult);
            }

            return BadRequest("Email or password is incorrect");
        }

        /// <summary>
        /// refresh token
        /// </summary>
        /// <param name="request">object includes refresh token string value</param>
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest request)
        {
            var userEmail = await _userService.GetEmailByRefreshToken(request.Token);
            if (!string.IsNullOrEmpty(userEmail))
            {
                var userId = _userService.GetUserByEmail(userEmail).Id;
                if (!await _refreshTokenService.IsRefreshTokenIsValidAsync(request.Token, userId))
                {
                    return BadRequest("Invalid Refresh Token");
                }

                var jwt = await GetJwt(userEmail);

                return Ok(jwt);
            }

            return BadRequest("Email or password is incorrect");
        }

        private async Task<JwtAuthResult> GetJwt(string email)
        {
            var roleName = _roleService.GetRoleNameByEmail(email);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, roleName)
            };

            var jwtResult = await _jwtAuthManager.GenerateTokens(email, claims);
            return jwtResult;
        }
    }
}
