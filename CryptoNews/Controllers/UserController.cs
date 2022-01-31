using Microsoft.AspNetCore.Mvc;
using CryptoNews.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private CryptoNewsContext cryptoNContext;

        public UserController(CryptoNewsContext context)
        {
            cryptoNContext = context;
        }

        [HttpGet]
        public IList<User> Get() =>
            GetInternal();
        private IList<User> GetInternal()
        {
            return (this.cryptoNContext.Users.ToList());
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
