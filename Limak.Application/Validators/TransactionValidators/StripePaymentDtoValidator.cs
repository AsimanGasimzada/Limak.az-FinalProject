using FluentValidation;
using Limak.Application.DTOs.StripeDTOs;

namespace Limak.Application.Validators.TransactionValidators;

public class StripePaymentDtoValidator : AbstractValidator<StripePayDto>
{
    public StripePaymentDtoValidator()
    {
        RuleFor(x => x.Email).NotEmpty().MaximumLength(256);
        RuleFor(x => x.FullName).NotEmpty().MaximumLength(128);
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(128);
        RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);

    }
}
