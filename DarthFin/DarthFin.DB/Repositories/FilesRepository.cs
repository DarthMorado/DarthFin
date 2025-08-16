using DarthFin.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace DarthFin.DB.Repositories
{
    public interface IFilesRepository : IBaseRepository<FileEntity>
    {
        public Task<List<FileEntity>> GetByUserIdAsync(int userId, CancellationToken cancellationToken);
    }

    public class FilesRepository : BaseRepository<FileEntity>, IFilesRepository
    {
        public FilesRepository(Database context)
            : base(context)
        {

        }

        public async Task<List<FileEntity>> GetByUserIdAsync(int userId, CancellationToken cancellationToken)
        {
            return await _dbSet.Where(x => x.UserId == userId).ToListAsync(cancellationToken);
        }
    }
}
