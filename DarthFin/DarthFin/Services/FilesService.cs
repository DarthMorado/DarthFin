using AutoMapper;
using CsvHelper;
using CsvHelper.Configuration;
using DarthFin.DB.Entities;
using DarthFin.DB.Repositories;
using DarthFin.Dto;
using DarthFin.Mapping.CSV;
using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace DarthFin.Services
{
    public interface IFilesService
    {
        public List<T> ReadCsv<T, M>(string data) where M : ClassMap<T>;

        public Task ProcessFileAsync(int fileId);
        public Task<List<FinEntryDto>> GetFinEntriesAsync(int fileId);
    }

    public class FilesService : IFilesService
    {
        private readonly IFilesRepository _filesRepository;
        private readonly IFinEntryRepository _finEntryRepository;
        private readonly IMapper _mapper;

        public FilesService(IFilesRepository filesRepository,
            IFinEntryRepository finEntryRepository,
            IMapper mapper)
        {
            _filesRepository = filesRepository;
            _finEntryRepository = finEntryRepository;
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

        public async Task<List<FinEntryDto>> GetFinEntriesAsync(int fileId)
        {
            var data = await _finEntryRepository.GetAllAsync();
            data = data.Where(x => x.FromFileId == fileId).ToList();
            return _mapper.Map<List<FinEntryDto>>(data);
        }

        public async Task ProcessFileAsync(int fileId)
        {
            //Get file
            var file = await _filesRepository.GetByIdAsync(fileId);

            //Read csv to FinEntries
            List<FinEntryDto> entries = new();
            if (file != null)
            {
                var stringContent = System.Text.Encoding.UTF8.GetString(file.Content);

                var swedbankEntries = ReadCsv<SwedbankCsvEntryDto, SwedbankCsvMapping>(stringContent);
                entries = _mapper.Map<List<FinEntryDto>>(swedbankEntries);

                foreach (var entry in entries)
                {
                    entry.FromFileId = fileId;
                    entry.UserId = file.UserId;
                    //entry.RealDate
                    string pattern = @"\d{1,2}\.\d{1,2}\.\d{4}";
                    Match match = Regex.Match(entry.Information, pattern);

                    if (match.Success)
                    {
                        string dateString = match.Value;

                        if (DateTime.TryParseExact(dateString, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                        {
                            entry.RealDate = date;
                        }
                    }
                }
            }

            //Delete existing
            await _finEntryRepository.DeleteByFile(fileId);

            //Write entries to db
            foreach (var entry in entries)
            {
                await _finEntryRepository.CreateAsync(_mapper.Map<FinEntryEntity>(entry));
            }

            //update file
            file.IsProcessed = true;
            _filesRepository.Update(file);

            await _filesRepository.SaveChangesAsync();
        }
    }
}
