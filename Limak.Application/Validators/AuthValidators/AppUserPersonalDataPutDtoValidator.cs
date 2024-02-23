using FluentValidation;
using Limak.Application.DTOs.AuthDTOs;

namespace Limak.Application.Validators.AuthValidators;

public class AppUserPersonalDataPutDtoValidator : AbstractValidator<AppUserPersonalDataPutDto>
{
    public AppUserPersonalDataPutDtoValidator()
    {
        RuleFor(x => x.SeriaNumber).NotNull().MaximumLength(11).MinimumLength(7);
        RuleFor(x => x.FinCode).NotNull().MaximumLength(7).MinimumLength(7);
        RuleFor(x => x.Location).NotNull().MaximumLength(128).MinimumLength(3);
        RuleFor(x => x.GenderId).NotNull().GreaterThan(0);
        RuleFor(x => x.CitizenshipId).NotNull().GreaterThan(0);

    }
}
