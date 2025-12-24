using AutoMapper;
using DarthFin.DB.Entities;
using DarthFin.DB.Repositories;
using DarthFin.Dto;

namespace DarthFin.Services
{
    public interface ICategoriesService
    {
        public Task<List<CategoryDto>> GetByUser(int userId);
        public Task<int> CreateCategory(CategoryDto category);
        public Task DeleteCategory(int id);
        public Task SaveCategory(CategoryDto category);
    }

    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _repository;
        private readonly IMapper _mapper;
        public CategoriesService(ICategoriesRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<CategoryDto>> GetByUser(int userId)
        {
            var allCategories = await _repository.GetAllAsync();
            var dbCategories = allCategories.Where(c => c.UserId == userId).ToList();
            var categories = _mapper.Map<List<CategoryDto>>(dbCategories);
            return categories;
        }
            
        public async Task<int> CreateCategory(CategoryDto category)
        {
            try
            {
                var entity = _mapper.Map<CategoryEntity>(category);
                await _repository.CreateAsync(entity);
                await _repository.SaveChangesAsync();

                return entity.Id;
            }
            catch
            {
                return 0;
            }
        }

        public async Task DeleteCategory(int id)
        {
            var children = await _repository.GetAllAsync(x => x.ParentCategoryId == id);
            foreach(var child in children)
            {
                await DeleteCategory(child.Id);
            }

            await _repository.Delete(id);
            await _repository.SaveChangesAsync();
        }

        public async Task SaveCategory(CategoryDto category)
        {
            var entity = _mapper.Map<CategoryEntity>(category);
            _repository.Update(entity);
            await _repository.SaveChangesAsync();
        }
    }
}
