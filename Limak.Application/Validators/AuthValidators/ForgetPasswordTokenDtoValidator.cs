using FluentValidation;
using Limak.Application.DTOs.AuthDTOs;

namespace Limak.Application.Validators.AuthValidators;

public class ForgetPasswordTokenDtoValidator : AbstractValidator<ForgetPasswordTokenDto>
{
    public ForgetPasswordTokenDtoValidator()
    {
        RuleFor(x => x.Token).NotNull();
        RuleFor(x => x.AppUserId).NotNull();
    }
}
