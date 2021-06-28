using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CryptoNews.Controllers
{
    public class CommentsController : Controller
    {
        private readonly ICommentService _commentService;
        private readonly IUserService _userService;

        public CommentsController(ICommentService commentSvc,
            IUserService userSvc)
        {
            _commentService = commentSvc;
            _userService = userSvc;
        }
        public IActionResult List(Guid newsId)
        {
            var comms = _commentService.GetCommentsByNewsId(newsId);
            return View(new CommentsListVM
            {
                NewsId = newsId,
                Comments = comms
            });
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CommentCreateVM vm)
        {
            var user = HttpContext.User.Claims.FirstOrDefault(c => c.Type.Equals(ClaimsIdentity.DefaultNameClaimType));
            var userEmail = user?.Value;
            var userId = _userService.GetUserByEmail(userEmail).Id;

            var dto = new CommentDto()
            {
                Id = Guid.NewGuid(),
                NewsId = vm.NewsId,
                Text = vm.CommText,
                CreateAt = DateTime.Now,
                UserId = userId,
                Rating = 0,
                ParentId = null
            };
            await _commentService.AddComment(dto);

            return Ok();
        }
    }
}
