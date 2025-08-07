using AutoMapper;
using DarthFin.DB.Repositories;
using DarthFin.Dto;

namespace DarthFin.Services
{
    public interface ICategoriesService
    {
        public Task<List<CategoryDto>> GetByUser(int userId);
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
            
    }
}
