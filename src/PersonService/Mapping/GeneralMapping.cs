using AutoMapper;
using PersonServices.Dto;
using PersonServices.Dtos;
using PersonServices.Model;

namespace PersonServices.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            CreateMap<Person, PersonCreateDto>().ReverseMap();
            CreateMap<Person, PersonDto>().ReverseMap();
            CreateMap<ContactInfo, ContactInfoDto>().ReverseMap();


        }
    }
}
