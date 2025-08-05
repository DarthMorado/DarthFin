using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using DarthFin.DB.Repositories;
using DarthFin.Dto;
using System;
using System.Globalization;

namespace DarthFin.Services
{
    public interface IFilesService
    {
        public List<T> ReadCsv<T, M>(string data) where M : ClassMap<T>;
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

        public List<T> ReadCsv<T,M>(string data) where M : ClassMap<T>
        {
            var result = new List<T>();
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    BadDataFound = null, // Ignore bad data
                    MissingFieldFound = null, // Optional: ignore missing fields
                    HeaderValidated = null,    // Optional: ignore header mismatches
                    Delimiter = ";"
                };

                using var reader = new StringReader(data);
                using var csv = new CsvReader(reader, config);
                csv.Context.RegisterClassMap<M>();

                result = csv.GetRecords<T>().ToList();
            }
            catch(Exception ex)
            {

            }
            return result;
        }

    }
}
