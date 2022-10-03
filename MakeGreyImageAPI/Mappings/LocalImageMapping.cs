using AutoMapper;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Entities;

namespace MakeGreyImageAPI.Mappings;
/// <summary>
/// 
/// </summary>
public class LocalImageMapping : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public LocalImageMapping()
    {
        CreateMap<LocalImageCreateDTO, LocalImage>().ReverseMap();
        CreateMap<LocalImageUpdateDTO, LocalImage>().ReverseMap();
        CreateMap<LocalImage, LocalImageDTO>().ReverseMap();
        CreateMap<LocalImageBaseDTO, LocalImage>().ReverseMap();
    }
}