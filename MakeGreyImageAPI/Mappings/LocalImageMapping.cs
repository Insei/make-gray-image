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
        CreateMap<LocalImageCreateDTO, LocalImage>().ReverseMap();
        CreateMap<LocalImage, LocalImageDTO>().ReverseMap();
        CreateMap<LocalImageDTO, LocalImage>().ReverseMap();
    }
}