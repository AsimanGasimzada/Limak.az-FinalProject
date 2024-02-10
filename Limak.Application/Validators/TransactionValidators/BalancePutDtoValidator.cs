using FluentValidation;
using Limak.Application.DTOs.TransactionDTOs;

namespace Limak.Application.Validators.TransactionValidators;

public class BalancePutDtoValidator:AbstractValidator<BalancePutDto>
{
    public BalancePutDtoValidator()
    {
        RuleFor(x=>x.Amount).NotEmpty().GreaterThanOrEqualTo(0);
    }
}
