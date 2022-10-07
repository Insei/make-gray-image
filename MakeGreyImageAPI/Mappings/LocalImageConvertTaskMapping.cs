using AutoMapper;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Entities;

namespace MakeGreyImageAPI.Mappings;
/// <summary>
/// Mapper of LocalImageConvertTask entities
/// </summary>
public class LocalImageConvertTaskMapping : Profile
{
    /// <summary>
    /// Constructor of LocalImageConvertTaskMapping class
    /// </summary>
    public LocalImageConvertTaskMapping()
    {
        CreateMap<LocalImageConvertTask, LocalImageConvertTaskDTO>().ReverseMap();
        CreateMap<LocalImageConvertTask, LocalImageConvertTaskCreateDTO>().ReverseMap();
    }
}