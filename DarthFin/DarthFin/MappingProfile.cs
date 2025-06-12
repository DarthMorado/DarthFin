using AutoMapper;
using DarthFin.DB.Entities;
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
            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, UserEntity>();
            CreateMap<UserEntity, UserDto>();

            CreateMap<FileEntity, FileDto>();
            CreateMap<FileEntity, FileModel>();
        }
    }
}
