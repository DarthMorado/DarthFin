using DarthFin.Dto;
using DarthFin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace DarthFin.Controllers
{
    public class BaseController : Controller
    {
        private readonly IUserService _userService;

        public BaseController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<UserDto?> GetUser(CancellationToken cancellationToken)
        {
            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!String.IsNullOrWhiteSpace(emailClaim))
            {
                var userInfo = await _userService.GetUserBasicInfoAsync(emailClaim, cancellationToken);
                return userInfo;
            }
            return null;
        }
    }
}
