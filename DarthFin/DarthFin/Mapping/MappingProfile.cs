using AutoMapper;
using DarthFin.DB.Entities;
using DarthFin.Dto;
using DarthFin.Models;

namespace DarthFin.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserDto, UserModel>();
            CreateMap<UserModel, UserDto>();
            CreateMap<UserDto, UserEntity>();
            CreateMap<UserEntity, UserDto>();

            CreateMap<CategoryDto, CategoryEntity>();
            CreateMap<CategoryEntity, CategoryDto>();

            CreateMap<FileEntity, FileDto>();
            CreateMap<FileEntity, FileModel>();

            CreateMap<FinEntryEntity, FinEntryDto>();
            CreateMap<FinEntryDto, FinEntryEntity>();

            CreateMap<SwedbankCsvEntryDto, FinEntryDto>()
                .ForMember(d => d.Account, x => x.MapFrom(src => src.AccountNumber))
                .ForMember(d => d.EntryType, x => x.MapFrom(src =>
                    src.EntryType == "10" ? FinEntryType.StartingAmount :
                    src.EntryType == "20" ? FinEntryType.Transaction :
                    src.EntryType == "82" ? FinEntryType.Circulation :
                    src.EntryType == "86" ? FinEntryType.EndAmount :
                    FinEntryType.None
            ))
                .ForMember(d => d.EntryDate, x => x.MapFrom(src => Convert.ToDateTime(src.Date)))
                .ForMember(d => d.RealDate, x => x.Ignore())
                .ForMember(d => d.Correspondent, x => x.MapFrom(src => src.Correspondent))
                .ForMember(d => d.Information, x => x.MapFrom(src => src.Information))
                .ForMember(d => d.Amount, x => x.MapFrom(src => Convert.ToDouble(src.Amount)))
                .ForMember(d => d.Currency, x => x.MapFrom(src => src.Currency))
                .ForMember(d => d.IsExpense, x => x.MapFrom(src => src.Direction.ToUpper() == "D"))
                .ForMember(d => d.ExternalId, x => x.MapFrom(src => src.ArchiveCode))
                .ForMember(d => d.DocumentNumber, x => x.MapFrom(src => src.DocumentNumber))
                ;
        }
    }
}
