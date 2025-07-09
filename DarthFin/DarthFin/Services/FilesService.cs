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
        //public Task<List<string[]>> ReadCsv(IFormFile file);
        public Task<List<string[]>> ReadCsv(string data);
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

        private void Foo()
        {
            

            using (var reader = new StreamReader("path\\to\\file.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<dynamic>();
                foreach (var record in records)
                {
                    // whatever u want
                }
            }
        }

        public async Task<List<string[]>> ReadCsv(string data)
        {
            try
            {
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    BadDataFound = null, // Ignore bad data
                    MissingFieldFound = null, // Optional: ignore missing fields
                    HeaderValidated = null    // Optional: ignore header mismatches
                };

                using var reader = new StringReader(data);
                using var csv = new CsvReader(reader, config);

                var records = csv.GetRecords<dynamic>().ToList();
                foreach (var record in records)
                {
                    // whatever u want

                }

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
            //var lines = data.Split(';');

            //if (lines is null || lines.Count() == 0)
            //{
            //    return null;
            //}

            //var headers = await ReadCsvLine(lines[0]);
            //if (headers is null || headers.Count() == 0);

            //var result = new List<string[]>();

            //return result;
        }

       //private async Task<List<string>> ReadCsvLine(string data)
       // {

       // }

    }
}
