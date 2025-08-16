using DarthFin.DB.Entities;

namespace DarthFin.DB.Repositories
{
    public interface IFinEntryRepository : IBaseRepository<FinEntryEntity> {
        Task DeleteByFile(int fileId);
    }

    public class FinEntryRepository : BaseRepository<FinEntryEntity>, IFinEntryRepository
    {
        public FinEntryRepository(Database context) 
            : base(context)
        {

        }

        public async Task DeleteByFile(int fileId)
        {
            var items = _dbSet.Where(x => x.FromFileId == fileId);
            foreach (var item in items)
            {
                _dbSet.Remove(item);
            }
        }
    }
}
