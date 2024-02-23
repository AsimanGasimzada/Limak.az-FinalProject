using FluentValidation;
using Limak.Application.DTOs.AuthDTOs;

namespace Limak.Application.Validators.AuthValidators;

public class AppUserAccountDataPutDtoValidator:AbstractValidator<AppUserAccountDataPutDto>
{
    public AppUserAccountDataPutDtoValidator()
    {
        RuleFor(x => x.Name).NotNull().MaximumLength(64).MinimumLength(2);
        RuleFor(x => x.Surname).NotNull().MaximumLength(64).MinimumLength(2);
        RuleFor(x => x.Email).NotNull().MaximumLength(256).MinimumLength(5);
        RuleFor(x => x.PhoneNumber).NotNull().MaximumLength(64).MinimumLength(5);
        RuleFor(x => x.Birtday).NotNull();
        RuleFor(x => x.WarehouseId).NotNull().GreaterThan(0);
    }
}