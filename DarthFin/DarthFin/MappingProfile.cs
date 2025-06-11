using AutoMapper;
using DarthFin.Dto;
using DarthFin.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DarthFin
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, UserModel>();
        }
    }
}
