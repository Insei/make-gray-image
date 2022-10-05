using AutoMapper;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Entities;

namespace MakeGreyImageAPI.Mappings;
/// <summary>
/// 
/// </summary>
public class LocalImageConvertTaskMapping : Profile
{
    /// <summary>
    /// 
    /// </summary>
    public LocalImageConvertTaskMapping()
    {
        CreateMap<LocalImageConvertTask, LocalImageConvertTaskDTO>().ReverseMap();
        CreateMap<LocalImageConvertTask, LocalImageConvertTaskCreateDTO>().ReverseMap();
    }
}