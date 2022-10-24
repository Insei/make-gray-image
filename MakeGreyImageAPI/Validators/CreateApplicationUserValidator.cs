using FluentValidation;
using MakeGreyImageAPI.DTOs;
using MakeGreyImageAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace MakeGreyImageAPI.Validators;

/// <summary>
/// Validation for ApplicationUserCreateDTO
/// </summary>
public class CreateApplicationUserValidator : AbstractValidator<ApplicationUserCreateDto>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private string _errors { get; set; } = "password validation error";

    /// <summary>
    /// Constructor of CreateApplicationUserValidator
    /// </summary>
    /// <param name="userManager">UserManager</param>
    public CreateApplicationUserValidator(UserManager<ApplicationUser> userManager)
    {
        var message = "";
        _userManager = userManager;
        RuleFor(d => d.Password)
            .Must((d, _) => PasswordIsValid(d.Password, out message)).WithMessage(_ => message);
    }
    /// <summary>
    /// method for verifying the user's password
    /// </summary>
    /// <param name="password">user password</param>
    /// <param name="errorMessage">message with error text</param>
    /// <returns>boolean</returns>
    private bool PasswordIsValid(string password, out string errorMessage)
    {
        var _passwordErrors = new List<string>();
        var validators = _userManager.PasswordValidators;

        foreach (var validator in validators)
        {
            var result = validator.ValidateAsync(_userManager, new ApplicationUser(), password).Result;

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    _passwordErrors.Add(error.Description);
                }
            }
        }
        errorMessage = string.Join(" ", _passwordErrors);
        if (_passwordErrors.Count > 0)
            return false;
        return true;
    }
}
    
