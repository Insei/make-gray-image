using AutoMapper;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Entities;

namespace MakeGreyImageAPI.Mappings;
/// <summary>
/// Mapper of LocalImage entities
/// </summary>
public class LocalImageMapping : Profile
{
    /// <summary>
    /// Constructor of LocalImageMapping class
    /// </summary>
    public LocalImageMapping()
    {
        CreateMap<LocalImageCreateDto, LocalImage>().ReverseMap();
        CreateMap<LocalImage, LocalImageDto>().ReverseMap();
        CreateMap<LocalImageDto, LocalImage>().ReverseMap();
    }
}