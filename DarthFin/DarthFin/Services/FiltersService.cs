using AutoMapper;
using DarthFin.DB.Entities;
using DarthFin.DB.Repositories;
using DarthFin.Dto;

namespace DarthFin.Services
{
    public interface IFiltersService
    {
        public Task CreateFilterAsync(FilterDto filter, bool applyToAllEntries, CancellationToken cancellationToken);
    }

    public class FiltersService : IFiltersService
    {
        private readonly IFiltersRepository _repository;
        private readonly IFinEntryRepository _entryRepository;
        private readonly IMapper _mapper;

        public FiltersService(IFiltersRepository repository,
            IFinEntryRepository finEntryRepository,
            IMapper mapper)
        {
            _repository = repository;
            _entryRepository = finEntryRepository;
            _mapper = mapper;
        }

        public async Task CreateFilterAsync(FilterDto filter, bool applyToAllEntries, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<FilterEntity>(filter);
            await _repository.CreateAsync(entity);
            await _repository.SaveChangesAsync();

            if (applyToAllEntries)
            {
                var entries = await _entryRepository.SearchAsync(entity, cancellationToken);
                foreach (var entry in entries)
                {
                    entry.CategoryId = entity.CategoryId;
                    _entryRepository.Update(entry);
                    await _entryRepository.SaveChangesAsync();
                }
            }
        }
    }
}
