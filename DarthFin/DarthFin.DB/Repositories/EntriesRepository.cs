using DarthFin.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace DarthFin.DB.Repositories
{
    public interface IFinEntryRepository : IBaseRepository<FinEntryEntity> {
        Task DeleteByFile(int fileId);
        public Task<List<FinEntryEntity>> SearchAsync(FilterEntity filter, CancellationToken cancellationToken);
        Task<List<FinEntryEntity>> SearchAsync(DateTime? from, DateTime? till, bool? isExpense, CancellationToken cancellationToken);
    }

    public class EntriesRepository : BaseRepository<FinEntryEntity>, IFinEntryRepository
    {
        public EntriesRepository(Database context) 
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

        public async Task<List<FinEntryEntity>> SearchAsync(FilterEntity filter, CancellationToken cancellationToken)
        {
            var query = _dbSet.AsQueryable();

            if (!String.IsNullOrWhiteSpace(filter.Correspondent))
            {
                query = query.Where(x => x.Correspondent != null && x.Correspondent.Contains(filter.Correspondent));
            }

            if (!String.IsNullOrWhiteSpace(filter.Information))
            {
                query = query.Where(x => x.Information != null && x.Information.Contains(filter.Correspondent));
            }

            if (filter.AmountFrom.HasValue)
            {
                query = query.Where(x => x.Amount >= filter.AmountFrom);
            }

            if (filter.AmountTill.HasValue)
            {
                query = query.Where(x => x.Amount <= filter.AmountTill);
            }

            if (filter.DateFrom.HasValue)
            {
                query = query.Where(x => (x.RealDate ?? x.EntryDate).Value >= filter.DateFrom);
            }

            if (filter.DateTill.HasValue)
            {
                query = query.Where(x => (x.RealDate ?? x.EntryDate).Value <= filter.DateTill);
            }

            query = query.OrderBy(x => (x.RealDate ?? x.EntryDate).Value);

            return await query.ToListAsync();
        }

        public async Task<List<FinEntryEntity>> SearchAsync(DateTime? from, DateTime? till, bool? isExpense, CancellationToken cancellationToken)
        {
            var query = _dbSet.AsQueryable();

            query = query.Include(x => x.Category);

            if (isExpense.HasValue)
            {
                query = query.Where(x => x.IsExpense == isExpense);
            }

            if (from.HasValue)
            {
                query = query.Where(x => (x.RealDate ?? x.EntryDate).Value >= from);
            }

            if (till.HasValue)
            {
                query = query.Where(x => (x.RealDate ?? x.EntryDate).Value <= till);
            }

            query = query.OrderBy(x => (x.RealDate ?? x.EntryDate).Value);

            return await query.ToListAsync();
        }
    }
}
