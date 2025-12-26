using AutoMapper;
using DarthFin.DB.Repositories;
using DarthFin.Dto;

namespace DarthFin.Services
{
    public interface IEntriesService
    {
        public Task<FinEntryDto> GetById(int entryId);
        public Task<List<FinEntryDto>> Search(DateTime from, DateTime till, CancellationToken cancellationToken);
    }

    public class EntriesService : IEntriesService
    {
        private readonly IFinEntryRepository _repository;
        private readonly IMapper _mapper;

        public EntriesService(IFinEntryRepository repository,
            IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<FinEntryDto> GetById(int entryId)
        {
            var entity = await _repository.GetByIdAsync(entryId);
            return _mapper.Map<FinEntryDto>(entity);
        }

        public async Task<List<FinEntryDto>> Search(DateTime from, DateTime till, CancellationToken cancellationToken)
        {
            var result = await _repository.SearchAsync(from, till, true, cancellationToken);
            //var result = await _repository.GetAllAsync(x => (x.IsExpense ?? false) &&
            //    (x.RealDate.HasValue ? (x.RealDate >= from && x.RealDate <= till) : (x.EntryDate >= from && x.EntryDate <= till))
            //);

            return result.Select(x => _mapper.Map<FinEntryDto>(x)).ToList();
        }
    }
}
