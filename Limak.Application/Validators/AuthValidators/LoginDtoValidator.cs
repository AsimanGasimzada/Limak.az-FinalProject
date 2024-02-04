using FluentValidation;
using Limak.Application.DTOs.AuthDTOs;

namespace Limak.Application.Validators.AuthValidators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Email).NotNull().MaximumLength(256);
        RuleFor(x => x.Email).NotNull().MaximumLength(64).MinimumLength(6);
    }
}
