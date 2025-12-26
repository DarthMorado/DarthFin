using DarthFin.DB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DarthFin.DB.Repositories
{
    public interface IFiltersRepository : IBaseRepository<FilterEntity>
    {

    }
    public class FiltersRepository : BaseRepository<FilterEntity>, IFiltersRepository
    {
        public FiltersRepository(Database context) : base(context)
        {
        }
    }
}
