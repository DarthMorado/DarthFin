using AutoMapper;
using DarthFin.Dto;
using DarthFin.Models;
using DarthFin.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Web.Mvc;
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

        
        public async Task<IActionResult> AddSubcategoryAsync(CategoriesItemModel model, CancellationToken cancellationToken)
        {
            List<CategoryDto> categories = new();

            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!String.IsNullOrWhiteSpace(emailClaim))
            {
                var userInfo = await _userService.GetUserBasicInfoAsync(emailClaim, cancellationToken);

                var newCategory = new Dto.CategoryDto()
                {
                    Color = "#818380",
                    Name = " ",
                    UserId = userInfo.Id,
                    ParentCategoryId = model.Category.Id
                };

                await _categoriesService.CreateCategory(newCategory);
            }

            return RedirectToAction("List");
        }
        
        public async Task<IActionResult> SaveAllAsync(CategoriesListModel model, CancellationToken cancellationToken)
        {
            //await _categoriesService.SaveCategory(model.Category);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> SaveAsync(CategoriesItemModel model, CancellationToken cancellationToken)
        {
            await _categoriesService.SaveCategory(model.Category);
            return RedirectToAction("List");
        }

        public async Task<IActionResult> DeleteAsync(CategoriesItemModel model, CancellationToken cancellationToken)
        {
            await _categoriesService.DeleteCategory(model.Category.Id);
            return RedirectToAction("List");
        }

        
        [HttpPost]
        public async Task<IActionResult> NewCategoryAsync(CancellationToken cancellationToken)
        {
            List<CategoryDto> categories = new();

            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!String.IsNullOrWhiteSpace(emailClaim))
            {
                var userInfo = await _userService.GetUserBasicInfoAsync(emailClaim, cancellationToken);

                var newCategory = new Dto.CategoryDto()
                {
                    Color = "#818380",
                    Name = " ",
                    UserId = userInfo.Id
                };

                await _categoriesService.CreateCategory(newCategory);

                categories = await _categoriesService.GetByUser(userInfo.Id);
            }

            var model = GetModel(categories);

            return View("List", model);
        }

        public async Task<IActionResult> ListAsync(CancellationToken cancellationToken)
        {
            var model = new CategoriesListModel();

            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!String.IsNullOrWhiteSpace(emailClaim))
            {
                var userInfo = await _userService.GetUserBasicInfoAsync(emailClaim, cancellationToken);
                var categories = await _categoriesService.GetByUser(userInfo.Id);
                //categories = new List<Dto.CategoryDto>()
                //{
                //    new Dto.CategoryDto()
                //    {
                //        Id=1,
                //        Name="Name",
                //    },
                //    new Dto.CategoryDto()
                //    {
                //        Id=2,
                //        Name="Name 2",
                //        ParentCategoryId = 1
                //    },
                //    new Dto.CategoryDto()
                //    {
                //        Id=3,
                //        Name="Name 3",
                //    }
                //};

                model = GetModel(categories);

            }

            return View(model);
        }

        private CategoriesListModel GetModel(List<CategoryDto> categories)
        {
            var result = new CategoriesListModel()
            {
                Categories = new()
            };

            Dictionary<int, List<CategoryDto>> children = new();
            foreach (var category in categories.Where(x => x.ParentCategoryId.HasValue))
            {
                if (!children.ContainsKey(category.ParentCategoryId.Value))
                {
                    children.Add(category.ParentCategoryId.Value, new());
                }
                children[category.ParentCategoryId.Value].Add(category);
            }

            foreach (var category in categories.Where(x => !x.ParentCategoryId.HasValue))
            {
                CategoriesItemModel item = GetItemModel(category, children);
                result.Categories.Add(item);
            }

            return result;
        }

        private CategoriesItemModel GetItemModel(CategoryDto category, Dictionary<int, List<CategoryDto>> children)
        {
            CategoriesItemModel result = new CategoriesItemModel()
            {
                Category = category,
                Children = new()
            };

            if (children.ContainsKey(category.Id))
            {
                foreach (var child in children[category.Id])
                {
                    CategoriesItemModel item = GetItemModel(child, children);
                    result.Children.Add(item);
                }
            }

            return result;
        }
    }
}
