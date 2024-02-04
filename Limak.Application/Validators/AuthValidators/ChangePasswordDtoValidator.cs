using FluentValidation;
using Limak.Application.DTOs.AuthDTOs;
using Microsoft.AspNetCore.Components;

namespace Limak.Application.Validators.AuthValidators;

public class ChangePasswordDtoValidator:AbstractValidator<ChangePasswordDto>
{
    public ChangePasswordDtoValidator()
    {
        RuleFor(x => x.ExistPassword).NotNull().MaximumLength(64).MinimumLength(6);
        RuleFor(x => x.NewPassword).NotNull().MaximumLength(64).MinimumLength(6);
        RuleFor(x => x.ConfirmNewPassword).NotNull().MaximumLength(64).MinimumLength(6);
        RuleFor(x => x.ConfirmNewPassword).Equal(x => x.NewPassword).WithMessage("The password and confirmation password do not match.");
    }
}
