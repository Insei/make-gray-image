using FluentValidation;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Validators;
/// <summary>
/// Validation for LocalImageConvertTaskCreateDTO
/// </summary>
public class CreateImageConvertTaskValidator : AbstractValidator<LocalImageConvertTaskCreateDto>
{
    private IGenericRepository<Guid, LocalImage> _imageRepository;
    /// <summary>
    /// Constructor of CreateImageConvertTaskValidator
    /// </summary>
    /// <param name="imageRepository">IGenericRepository</param>
    public CreateImageConvertTaskValidator(IGenericRepository<Guid, LocalImage> imageRepository)
    {
        _imageRepository = imageRepository;
        RuleFor(d => d.ImageId)
            .Must(IsImageExist).WithMessage("Image with this ID {PropertyValue} does not exist");
    }
    /// <summary>
    /// Checking for the existence of an image
    /// </summary>
    /// <param name="id">Image Id</param>
    /// <returns>Boolean</returns>
    private bool IsImageExist(Guid id)
    {
        return _imageRepository.GetById(id).Result != null;
    }
}