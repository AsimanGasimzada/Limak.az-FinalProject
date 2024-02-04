using FluentValidation;
using Limak.Application.DTOs.AuthDTOs;

namespace Limak.Application.Validators.AuthValidators;

public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(64).MinimumLength(2);
        RuleFor(x => x.Surname).NotNull().MaximumLength(64).MinimumLength(2);
        RuleFor(x => x.SeriaNumber).NotNull().MaximumLength(11).MinimumLength(7);
        RuleFor(x => x.Email).NotNull().MaximumLength(256).MinimumLength(5);
        RuleFor(x => x.PhoneNumber).NotNull().MaximumLength(64).MinimumLength(5);
        RuleFor(x => x.Password).NotNull().MaximumLength(64).MinimumLength(6);
        RuleFor(x => x.ConfirmPassword).NotNull().MaximumLength(64).MinimumLength(6);
        RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("The password and confirmation password do not match.");
        RuleFor(x => x.FinCode).NotNull().MaximumLength(7).MinimumLength(7);
        RuleFor(x => x.Birtday).NotNull();
        RuleFor(x => x.Location).NotNull().MaximumLength(128).MinimumLength(3);
        RuleFor(x => x.GenderId).NotNull();
        RuleFor(x => x.CitizenshipId).NotNull();
        RuleFor(x => x.UserPositionId).NotNull();
        RuleFor(x => x.WarehouseId).NotNull();


    }
}
