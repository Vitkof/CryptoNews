using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CryptoNews.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public CommentController(ICommentService commSvc,
                                 IUserService userSvc)
        {
            _commentService = commSvc;
            _userService = userSvc;
        }

        // GET: api/Comment/id
        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            try
            {
                var comment = _commentService.GetCommentById(id);
                if (comment == null) return NotFound();
                return Ok(comment);
            }
            catch(Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(CommentDto dto)
        {
            try
            {
                dto.Id = new Guid();
                var userEmail = HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type.Equals(ClaimTypes.Email))?.Value;
                var user = _userService.GetUserByEmail(userEmail);
                dto.CreateAt = DateTime.Now;

                var res = _commentService.AddComment(dto);
                return Created($"/api/comment/{dto.Id}", dto);
            }
            catch(Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(CommentDto dto)
        {
            try
            {
                var res = await _commentService.EditComment(dto);
                if (res == 0)
                    return BadRequest("Comment doesn't edited");

                return Ok("Comment succeeded edited");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error {ex.Message}");
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(Guid id, [FromBody] JsonPatchDocument<CommentDto> patchDoc)
        {
            try
            {
                if (patchDoc != null)
                {
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }

                    var comment = _commentService.GetCommentById(id);
                    if (comment == null)
                    {
                        return NotFound();
                    }

                    patchDoc.ApplyTo(comment);
                    await _commentService.EditComment(comment);
                    return new ObjectResult(comment);
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                Log.Error($"PATCH error updating Comment: {ex}");
            }
            return BadRequest("Error updating Comment");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var comment = _commentService.GetCommentById(id);
                var res = await _commentService.DeleteComment(comment);
                if (res == 0)
                {
                    return BadRequest("Comment doesn't Deleted");

                }
                return Ok("Comment succeeded deleted");
            }
            catch (Exception e)
            {
                return BadRequest($"Error {e.Message}");
            }
        }
    }
}
