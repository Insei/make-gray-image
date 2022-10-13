using FluentValidation;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Entities;
using MakeGreyImageAPI.Interfaces;

namespace MakeGreyImageAPI.Validators;
/// <summary>
/// 
/// </summary>
public class CreateImageConvertTaskValidator : AbstractValidator<LocalImageConvertTaskCreateDTO>
{
    private readonly IGenericRepository _repository;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="repository"></param>
    public CreateImageConvertTaskValidator(IGenericRepository repository)
    {
        _repository = repository;

        RuleFor(d => d.ImageId)
            .Must(IsImageExist).WithMessage("Image with this ID {PropertyName} does not exist");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private bool IsImageExist(Guid id)
    {
        return _repository.GetById<LocalImage>(id).Result != null;
    }
}