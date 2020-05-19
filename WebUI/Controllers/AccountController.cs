using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Abstract;
using Core.Entities.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace WebUI.Controllers
{
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;

        public AccountController(IUserService userService, IAuthService authService, IHttpContextAccessor httpContextAccessor, ICompositeViewEngine compositeViewEngine) : base(httpContextAccessor, compositeViewEngine)
        {
            _userService = userService;
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {

            var userLogin = _authService.Login(loginDto);
            if (userLogin.Success)
            {
                var user = userLogin.Data;
                var userRoles = _userService.GetUserClaims(user);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.Email,user.Email),
                    new Claim(ClaimTypes.Name,$"{user.FirstName} {user.LastName}")

                };

                userRoles.Select(x => x.Name).ToList()
                    .ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));
                
                var userIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Home");
            }

            return View(loginDto);
        }

        public IActionResult Register()
        {
            return View();
        }

        /*
        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            return View(registerDto);
        }
        */

    }
}