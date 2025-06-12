using AutoMapper;
using DarthFin.DB.Repositories;
using DarthFin.Dto;

namespace DarthFin.Services
{
    public interface IFilesService
    {

    }

    public class FilesService : IFilesService
    {
        private readonly IFilesRepository _filesRepository;
        private readonly IMapper _mapper;

        public FilesService(IFilesRepository filesRepository,
            IMapper mapper)
        {
            _filesRepository = filesRepository;
            _mapper = mapper;
        }

        public async Task<List<FileDto>> GetFilesByUser(int userId, CancellationToken cancellationToken)
        {
            var dbUsers = await _filesRepository.GetByUserIdAsync(userId, cancellationToken);
            var users = new List<FileDto>();
            if (dbUsers is not null && dbUsers.Any())
            {
                users = dbUsers.Select(x => _mapper.Map<FileDto>(x)).ToList();
            }
            return users;
        }
    }
}
