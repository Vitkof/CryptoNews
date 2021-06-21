using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptoNews.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userSvc)
        {
            _userService = userSvc;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM regVM)
        {
            if (ModelState.IsValid)
            {
                var passwordHash = _userService.GetHashPassword(regVM.Password);
                if (await _userService.AddUser(new UserDto()
                {
                    Id = Guid.NewGuid(),
                    Email = regVM.Email,
                    PasswordHash = passwordHash
                }))
                {
                    return RedirectToAction("Index", "Home");
                }
                else return BadRequest(regVM);
            }
   
            return View(regVM);
        }
    }
}
