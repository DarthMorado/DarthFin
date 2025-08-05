using AutoMapper;
using DarthFin.DB.Entities;
using DarthFin.DB.Repositories;
using DarthFin.Dto;
using DarthFin.Mapping.CSV;
using DarthFin.Models;
using DarthFin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading;

namespace DarthFin.Controllers
{
    public class FilesController : BaseController
    {
        private readonly IFilesRepository _filesRepository;
        private readonly IUserService _userService;
        private readonly IFilesService _filesService;
        private readonly IMapper _mapper;

        public FilesController(IFilesRepository filesRepository,
            IUserService userService,
            IFilesService filesService,
            IMapper mapper)
            : base(userService)
        {
            _filesRepository = filesRepository;
            _userService = userService;
            _filesService = filesService;
            _mapper = mapper;
        }

        public async Task<ActionResult> ListAsync(CancellationToken cancellationToken)
        {
            var model = new FileListModel();

            var emailClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!String.IsNullOrWhiteSpace(emailClaim))
            {
                var userInfo = await _userService.GetUserBasicInfoAsync(emailClaim, cancellationToken);
                var files = await _filesRepository.GetByUserIdAsync(userInfo.Id, cancellationToken);

                model = new()
                {
                    UserId = userInfo.Id,
                    Files = files.Select(x => _mapper.Map<FileModel>(x)).ToList()
                };
            }

            return View(model);
        }

        public async Task<IActionResult> ProcessAsync(int id)
        {
            var file = await _filesRepository.GetByIdAsync(id);
            
            if (file != null)
            {
                var stringContent = System.Text.Encoding.UTF8.GetString(file.Content);

                var swedbankEntries = _filesService.ReadCsv<SwedbankCsvEntryDto, SwedbankCsvMapping>(stringContent);
                var entries = _mapper.Map<List<FinEntryDto>>(swedbankEntries);

                foreach (var entry in entries)
                {
                    entry.FromFileId = id;
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
            return RedirectToAction("List", "Files");
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _filesRepository.Delete(id);
            await _filesRepository.SaveChangesAsync();
            return RedirectToAction("List", "Files");
        }

        public async Task<IActionResult> UploadFile(IFormFile file, CancellationToken cancellationToken)
        {
            var user = await GetUser(cancellationToken);

            if (file != null && file.Length > 0 && ((user?.Id ?? 0) != 0))
            {
                var fe = new FileEntity()
                {
                    IsProcessed = false,
                    UserId = user.Id,
                    Name = file.FileName
                };

                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    fe.Content = memoryStream.ToArray();
                }

                await _filesRepository.CreateAsync(fe);
                await _filesRepository.SaveChangesAsync();
            }
            else
            {
                //ViewBag.Message = "No file selected.";
            }

            return RedirectToAction("List", "Files");
        }
    }
}
