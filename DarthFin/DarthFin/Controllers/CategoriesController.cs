using AutoMapper;
using DarthFin.Models;
using DarthFin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace DarthFin.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IUserService _userService;
        private readonly ICategoriesService _categoriesService;
        private readonly IMapper _mapper;

        public CategoriesController(IUserService userService,
            ICategoriesService categoriesService,
            IMapper mapper
            )
        {
            _userService = userService;
            _categoriesService = categoriesService;
            _mapper = mapper;
        }

        public async Task<IActionResult> ListAsync(CancellationToken cancellationToken)
        {
            var model = new CategoriesListModel();

            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!String.IsNullOrWhiteSpace(emailClaim))
            {
                var userInfo = await _userService.GetUserBasicInfoAsync(emailClaim, cancellationToken);
                var categories = await _categoriesService.GetByUser(userInfo.Id);
                model.Categories = categories;
                
            }

            //test'
            model = new CategoriesListModel()
            {
                Categories = new List<Dto.CategoryDto>()
                {
                    new Dto.CategoryDto()
                    {
                        Id=1,
                        Name="Name",
                    },
                    new Dto.CategoryDto()
                    {
                        Id=2,
                        Name="Name 2",
                    },
                    new Dto.CategoryDto()
                    {
                        Id=3,
                        Name="Name 3",
                    }
                }
            };

            return View(model);
        }
    }
}
