using DarthFin.Models;
using DarthFin.Models.Graph;
using DarthFin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace DarthFin.Controllers
{
    public class GraphController : Controller
    {
        private readonly IUserService _userService;
        private readonly IGraphService _graphService;

        public GraphController(IUserService userService,
            IGraphService graphService)
        {
            _userService = userService;
            _graphService = graphService;
        }

        public async Task<IActionResult> TestAsync(CancellationToken cancellationToken)
        {
            var model = new BarGraphModel();

            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!String.IsNullOrWhiteSpace(emailClaim))
            {
                var userInfo = await _userService.GetUserBasicInfoAsync(emailClaim, cancellationToken);

                model = await _graphService.GetMonthBarData(userInfo.Id, 2025, 06);
            }

             return View("TestGraph", model);
            //return View("EmptyTestGraph", model);
        }
    }
}
