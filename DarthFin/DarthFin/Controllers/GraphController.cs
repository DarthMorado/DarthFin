using DarthFin.Models;
using DarthFin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace DarthFin.Controllers
{
    public class GraphController : Controller
    {
        private readonly IUserService _userService;

        public GraphController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> TestAsync(CancellationToken cancellationToken)
        {
            var model = new LineGraphModel();

            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!String.IsNullOrWhiteSpace(emailClaim))
            {
                var userInfo = await _userService.GetUserBasicInfoAsync(emailClaim, cancellationToken);
                

            }
            return View("TestGraph", model);
        }
    }
}
