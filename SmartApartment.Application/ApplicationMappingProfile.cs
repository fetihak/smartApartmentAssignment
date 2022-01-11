using AutoMapper;
using SmartApartment.Application.Contract.DTO;
using SmartApartment.Domain.Entity;

namespace SmartApartment.Application
{
    public class ApplicationMappingProfile : Profile
    {
        public ApplicationMappingProfile()
        {
            CreateMap<SearchResponse, SearchResponseDTO>().ForMember(i => i.type, j
                => j.MapFrom(map => map.type.ToString()));
        }

    }
}
