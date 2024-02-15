using FluentValidation;
using Limak.Application.DTOs.AuthDTOs;

namespace Limak.Application.Validators.AuthValidators;

public class ResetPasswordTokenDtoValidator : AbstractValidator<ResetPasswordTokenDto>
{
    public ResetPasswordTokenDtoValidator()
    {
        RuleFor(x => x.Token).NotNull();
        RuleFor(x => x.AppUserId).NotNull();
        RuleFor(x => x.Password).NotNull().MaximumLength(64).MinimumLength(6);
        RuleFor(x => x.ConfirmPassword).NotNull().MaximumLength(64).MinimumLength(6);
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("The password and confirmation password do not match.");
    }
}
