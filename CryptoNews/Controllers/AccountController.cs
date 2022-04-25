using CryptoNews.Core.DTO;
using CryptoNews.Core.IServices;
using CryptoNews.Models.ViewModels;
using CryptoNews.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Runtime.Versioning;

namespace CryptoNews.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountController(IUserService userSvc,
                                 IRoleService roleSvc,
                                 IHttpContextAccessor http)
        {
            _userService = userSvc;
            _roleService = roleSvc;
            _contextAccessor = http;
        }


        [Route("captcha-image")]
        [SupportedOSPlatform("windows")]
        public IActionResult CaptchaImage()
        {
            int width = 120;
            int height = 60;
            var captchaCode = Captcha.CreateCaptchaCode();
            var result = Captcha.CreateCaptchaImage(width, height, captchaCode);
            _contextAccessor.HttpContext.Session.SetString("Captcha", result.CaptchaCode);
            Stream stream = new MemoryStream(result.CaptchaBytes);
            return new FileStreamResult(stream, "image/png");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public Task<IActionResult> Register(RegisterVM vm) =>
            RegisterInternal(vm);
        private async Task<IActionResult> RegisterInternal(RegisterVM regVM)
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
                    PasswordHash = passHash,
                    PhoneNumber = regVM.PhoneNumber,
                    Gender = regVM.Gender,
                    ShortDescription = regVM.ShortDescription
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
        [SupportedOSPlatform("windows")]
        public async Task<IActionResult> Login(LoginVM logVM)
        {
            if (ModelState.IsValid)
            {
                if (Captcha.ValidateCaptchaCode(logVM.CaptchaCode, _contextAccessor.HttpContext))
                {
                    var userDto = _userService.GetUserByEmail(logVM.Email);
                    if (userDto != null)
                    {
                        var passHash = _userService.GetHashPassword(logVM.Password);
                        if (passHash.Equals(userDto.PasswordHash))
                        {
                            userDto.LastLoginDate = DateTime.Now;
                            await _userService.EditUser(userDto);

                            await Authenticate(userDto);
                            return string.IsNullOrEmpty(logVM.ReturnUrl)
                                ? RedirectToAction("Index", "Home")
                                : Redirect(logVM.ReturnUrl);
                        }
                        else ModelState.AddModelError("", "Invalid password");
                    }
                    else ModelState.AddModelError("", "Invalid email");
                }
                else ModelState.AddModelError("", "Invalid captcha");
            }

            return View(logVM);
        }

        [HttpGet]
        public Task<IActionResult> LogOut() =>
            LogoutInternal();
        private async Task<IActionResult> LogoutInternal()
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
        public IActionResult Settings() =>
            SettingsInternal();
        private IActionResult SettingsInternal()
        {
            var user = HttpContext.User.Claims.FirstOrDefault(cl => 
            cl.Type.Equals(ClaimsIdentity.DefaultNameClaimType));

            if (user != null) return View("LogoutInfo");
            return View("LoginInfo");
        }
    }
}
