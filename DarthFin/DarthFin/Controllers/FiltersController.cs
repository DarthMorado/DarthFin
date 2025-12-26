using DarthFin.Dto;
using DarthFin.Services;
using Microsoft.AspNetCore.Mvc;

namespace DarthFin.Controllers
{
    public class FiltersController : Controller
    {
        private readonly IEntriesService _entriesService;
        private readonly IFiltersService _filtersService;
        private readonly ICategoriesService _categoriesService;

        public FiltersController(IEntriesService entriesService,
            ICategoriesService categoriesService,
            IFiltersService filtersService)
        {
            _entriesService = entriesService;
            _categoriesService = categoriesService;
            _filtersService = filtersService;
        }

        [HttpGet]
        public async Task<IActionResult> NewFilterModal(int entryId)
        {
            var entry = await _entriesService.GetById(entryId);
            FilterDto model = new()
            {
                Correspondent = entry.Correspondent,
                Information = entry.Information
            };
            model.Categories = await _categoriesService.GetAllAsync();

            return PartialView("_NewFilterPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(FilterDto model, CancellationToken cancellationToken)
        {
            await _filtersService.CreateFilterAsync(model, true, cancellationToken);
            return NoContent();
        }
    }
}
