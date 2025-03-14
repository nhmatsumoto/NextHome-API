using AutoMapper;
using NextHome.Application.DTOs;
using NextHome.Domain.Entities;

namespace NextHome.Application.Mappings;

public class PropertyMappingProfile : Profile
{
    public PropertyMappingProfile()
    {
        CreateMap<Property, PropertyDTO>()
            .ForMember(dest => dest.Type, opt => 
                opt.MapFrom(src => src.Type.ToString()))
            .ForMember(dest => dest.FullAddress, opt =>
                opt.MapFrom(src => $"{src.Address.Street}, {src.Address.City}"))
            .ForMember(dest => dest.PhotoUrls, opt =>
                opt.MapFrom(src => src.Photos != null ? src.Photos.Select(p => p.Url).ToList() : new List<string>()))
            .ReverseMap();

        CreateMap<PropertyPhoto, PropertyPhotoDTO>()
            .ReverseMap();

        CreateMap<PropertyAddress, PropertyAddressDTO>().ReverseMap();
    }
}
