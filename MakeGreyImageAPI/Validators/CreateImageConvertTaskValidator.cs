using FluentValidation;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Validators;
/// <summary>
/// Validation for LocalImageConvertTaskCreateDTO
/// </summary>
public class CreateImageConvertTaskValidator : AbstractValidator<LocalImageConvertTaskCreateDTO>
{
    private readonly IGenericRepository _repository;
    /// <summary>
    /// Constructor of CreateImageConvertTaskValidator
    /// </summary>
    /// <param name="repository">IGenericRepository</param>
    public CreateImageConvertTaskValidator(IGenericRepository repository)
    {
        _repository = repository;
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
        return _repository.GetById<LocalImage>(id).Result != null;
    }
}