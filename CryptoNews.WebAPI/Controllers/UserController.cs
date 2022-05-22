using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Threading.Tasks;

namespace CryptoNews.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Get([FromBody] UserDto user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.Email))
                {
                    var getAll = _userService.GetUsers();
                    return Ok(getAll);
                }

                var userByEmail = _userService.GetUserByEmail(user.Email);
                return Ok(userByEmail);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return NotFound(e.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var user = _userService.GetUserById(id);

                if (user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                return NotFound();
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(UserDto dto)
        {
            try
            {
                var res = await _userService.EditUser(dto);
                if (res == 0)
                    return BadRequest("User doesn't updated");

                return Ok("User succeeded updated");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<UserDto> patchDoc)
        {
            try
            {
                if (patchDoc != null)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var user = _userService.GetUserById(id);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    patchDoc.ApplyTo(user);
                    await _userService.EditUser(user);
                    return new ObjectResult(user);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"PATCH error updating User: {ex}");
            }
            return BadRequest("Error updating User");
        }
    }
}
