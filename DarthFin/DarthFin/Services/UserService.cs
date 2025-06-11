using AutoMapper;
using DarthFin.DB.Entities;
using DarthFin.DB.Repositories;
using DarthFin.Dto;

namespace DarthFin.Services
{
    public interface IUserService
    {
        public Task<UserDto> GetUserBasicInfoAsync(string gmail, CancellationToken cancellationToken);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;


        public UserService(IUserRepository userRepository,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<UserDto> GetUserBasicInfoAsync(string gmail, CancellationToken cancellationToken)
        {
            UserEntity user = await _userRepository.GetByEmailAsync(gmail, cancellationToken);
            if (user == null)
            {
                user = new()
                {
                    Gmail = gmail,
                    IsAdmin = false
                };

                await _userRepository.CreateAsync(user);
                await _userRepository.SaveChangesAsync();
            }

            var result = _mapper.Map<UserDto>(user);
            return result;
        }
    }
}
