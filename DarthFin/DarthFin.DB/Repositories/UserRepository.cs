using DarthFin.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace DarthFin.DB.Repositories
{
    public interface IUserRepository : IBaseRepository<UserEntity>
    {
        public Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken);
    }

    public class UserRepository : BaseRepository<UserEntity>, IUserRepository
    {
        public UserRepository(Database context)
            : base(context)
        {

        }

        public async Task<UserEntity> GetByEmailAsync(string email, CancellationToken cancellationToken)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Gmail == email, cancellationToken);
        }
    }
}
