using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System;
using DarthFin.Services;
using AutoMapper;
using DarthFin.Models;

namespace DarthFin.Controllers
{
    public class AuthController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService,
            IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<IActionResult> IndexAsync(CancellationToken cancellationToken)
        {
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!String.IsNullOrWhiteSpace(emailClaim))
            {
                var userInfo = await _userService.GetUserBasicInfoAsync(emailClaim, cancellationToken);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("Index")
            };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet("user")]
        public async Task<IActionResult> UserSettings(CancellationToken cancellationToken)
        {
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!String.IsNullOrWhiteSpace(emailClaim))
            {
                var userInfo = await _userService.GetUserBasicInfoAsync(emailClaim, cancellationToken);
                return View("User", _mapper.Map<UserModel>(userInfo));
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

        }
    }
}
