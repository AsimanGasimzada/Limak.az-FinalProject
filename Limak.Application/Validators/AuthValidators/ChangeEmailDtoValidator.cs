using FluentValidation;
using Limak.Application.DTOs.AuthDTOs;

namespace Limak.Application.Validators.AuthValidators;

public class ChangeEmailDtoValidator : AbstractValidator<ChangeEmailDto>
{
    public ChangeEmailDtoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Email).NotNull().MaximumLength(256);
        RuleFor(x => x.Token).NotNull().MaximumLength(512);

    }
}
