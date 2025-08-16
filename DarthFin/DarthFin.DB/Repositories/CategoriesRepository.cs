using DarthFin.DB.Entities;

namespace DarthFin.DB.Repositories
{
    public interface ICategoriesRepository : IBaseRepository<CategoryEntity>
    {

    }

    public class CategoriesRepository : BaseRepository<CategoryEntity>, ICategoriesRepository
    {
        public CategoriesRepository(Database context)
            :base(context)
        {
            
        }
    }
}
