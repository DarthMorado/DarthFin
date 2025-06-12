using AutoMapper;
using DarthFin.DB.Repositories;
using DarthFin.Models;
using DarthFin.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading;

namespace DarthFin.Controllers
{
    public class FilesController : Controller
    {
        private readonly IFilesRepository _filesRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public FilesController(IFilesRepository filesRepository,
            IUserService userService,
            IMapper mapper)
        {
            _filesRepository = filesRepository;
            _userService = userService;
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
    }
}
