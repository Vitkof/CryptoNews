using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.Models.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CryptoNews.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AccountController(IUserService userSvc,
            IRoleService roleSvc)
        {
            _userService = userSvc;
            _roleService = roleSvc;
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
                var passHash = _userService.GetHashPassword(regVM.Password);
                if (await _userService.AddUser(new UserDto()
                {
                    Id = Guid.NewGuid(),
                    FirstName = regVM.FirstName,
                    LastName = regVM.LastName,
                    Email = regVM.Email,
                    PasswordHash = passHash
                }))
                {
                    return RedirectToAction("Index", "Home");
                }
                else return BadRequest(regVM);
            }
   
            return View(regVM);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            var model = new LoginVM() 
            { ReturnUrl = returnUrl };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM logVM)
        {
            if (ModelState.IsValid)
            {
                var userDto = _userService.GetUserByEmail(logVM.Email);
                if (userDto != null)
                {
                    var passHash = _userService.GetHashPassword(logVM.Password);
                    if (passHash.Equals(userDto.PasswordHash))
                    {
                        await Authenticate(userDto);
                        return string.IsNullOrEmpty(logVM.ReturnUrl)
                            ? RedirectToAction("Index", "Home")
                            : Redirect(logVM.ReturnUrl);
                    }
                }
                else NotFound();
            }

            return View(logVM);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private async Task Authenticate(UserDto userDto)
        {
            try
            {
                const string authType = "ApplicationCookie";
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, userDto.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, _roleService.GetRoleByUserId(userDto.Id).Name),
                 //   new Claim("age", userDto.Age.ToString()),
                };

                var identity = new ClaimsIdentity(claims,
                    authType,
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        [HttpGet]
        public IActionResult Settings()
        {
            var user = HttpContext.User.Claims.FirstOrDefault(cl => 
            cl.Type.Equals(ClaimsIdentity.DefaultNameClaimType));

            if (user != null) return View("LogoutInfo");
            return View("LoginInfo");
        }
    }
}
