using Microsoft.AspNetCore.Mvc;
using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoNews.Core.IServices;
using CryptoNews.Core.DTO;

namespace CryptoNews.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userSvc)
        {
            _userService = userSvc;
        }

        [HttpGet]
        public IList<UserDto> Get() =>
            GetInternal();
        private IList<UserDto> GetInternal()
        {
            return _userService.GetUsers().ToList();
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
