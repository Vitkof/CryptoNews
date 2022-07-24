using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.WebAPI.Auth;
using CryptoNews.WebAPI.Requests;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
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


        public AuthController(IUserService userSvc,
                              IRoleService roleService,
                              IJwtAuthManager authManager)
        {
            _userService = userSvc;
            _roleService = roleService;
            _jwtAuthManager = authManager;
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
                    PasswordHash = passwordHash,
                    RoleId = _roleService.GetIdByName("User")
                });

                if (isRegistered)
                {
                    var jwt = await _jwtAuthManager.GetJwt(request.Email);
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
        [ProducesResponseType(200, Type = typeof(JwtAuthResult))]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (request is null)
            {
                return BadRequest("Invalid client request");
            }

            var user = _userService.GetUserByEmail(request.Email);
            if (user is null)
            {
                return BadRequest("No user");
            }

            var passwordHash = _userService.GetHashPassword(request.Password);
            var isValid = await _userService.CheckAuthIsValid(new UserDto() 
            { 
                Email = request.Email, 
                PasswordHash = passwordHash 
            });

            if (isValid)
            {
                var jwtAuthResult = await _jwtAuthManager.GetJwt(request.Email);
                return Ok(jwtAuthResult);
            }

            return BadRequest("Email or password is incorrect");
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(GetResult))]
        public IActionResult GetResult()
        {
            return Ok("API Validated");
        }
    }
}
