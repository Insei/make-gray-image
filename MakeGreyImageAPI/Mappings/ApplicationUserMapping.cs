using AutoMapper;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Entities;

namespace MakeGreyImageAPI.Mappings;
/// <summary>
/// mapper of ApplicationUser entities
/// </summary>
public class ApplicationUserMapping :Profile
{   /// <summary>
    /// constructor of ApplicationUserMapping class
    /// </summary>
    public ApplicationUserMapping()
    {
        CreateMap<ApplicationUser, ApplicationUserDto>().ReverseMap();
        CreateMap<ApplicationUser, ApplicationUserCreateDto>().ReverseMap();
        CreateMap<ApplicationUser, ApplicationUserUpdateDto>().ReverseMap();
    }
}