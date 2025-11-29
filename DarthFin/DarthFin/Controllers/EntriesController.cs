using Microsoft.AspNetCore.Mvc;

namespace DarthFin.Controllers
{
    public class EntriesController : Controller
    {
        public EntriesController()
        {
            
        }

        public async Task<IActionResult> IndexAsync()
        {
            return View();
        }
    }
}
