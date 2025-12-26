using DarthFin.Models;
using DarthFin.Services;
using Microsoft.AspNetCore.Mvc;

namespace DarthFin.Controllers
{
    public class EntriesController : Controller
    {
        private readonly IEntriesService _entriesService;

        public EntriesController(IEntriesService entriesService)
        {
            _entriesService = entriesService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var model = new EntriesSearchModel();
            if (model.DateFrom == DateTime.MinValue)
            {
                model.DateFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }
            if (model.DateTill == DateTime.MinValue)
            {
                model.DateTill = model.DateFrom.AddMonths(1);
            }
            return View(model);
        }

        public async Task<IActionResult> SearchAsync(EntriesSearchModel model, CancellationToken cancellationToken)
        {
            var entries = await _entriesService.Search(model.DateFrom, model.DateTill, cancellationToken);
            var newModel = new EntriesListModel()
            {
                Entries = entries
            };
            return PartialView("_SearchResultPartial", newModel);
        }
    }
}
