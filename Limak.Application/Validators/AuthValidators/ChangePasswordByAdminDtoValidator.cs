using FluentValidation;
using Limak.Application.DTOs.AuthDTOs;

namespace Limak.Application.Validators.AuthValidators;

public class ChangePasswordByAdminDtoValidator : AbstractValidator<ChangePasswordByAdminDto>
{
    public ChangePasswordByAdminDtoValidator()
    {
        RuleFor(x => x.AppUserId).NotNull().GreaterThan(0);
        RuleFor(x => x.NewPassword).NotNull().MaximumLength(64).MinimumLength(6);
        RuleFor(x => x.ConfirmNewPassword).NotNull().MaximumLength(64).MinimumLength(6);
        RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword).WithMessage("The password and confirmation password do not match.");
    }
}
