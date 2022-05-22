using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoNews.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TestController : ControllerBase
    {
        [HttpGet("default")]
        public IActionResult GetDefault()
        {
            return Ok("Authorized User");
        }

        [HttpGet("admin")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdmin()
        {
            return Ok("Admin User");
        }

        [HttpGet("unauthorized")]
        [AllowAnonymous]
        public IActionResult GetUnauthorized()
        {
            return Ok("unauthorized User");
        }
    }
}
