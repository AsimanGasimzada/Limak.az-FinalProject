using FluentValidation;
using Limak.Application.DTOs.TransactionDTOs;

namespace Limak.Application.Validators.TransactionValidators;

public class BalancaPutDtoValidator:AbstractValidator<BalancePutDto>
{
    public BalancaPutDtoValidator()
    {
        RuleFor(x=>x.Amount).NotEmpty().GreaterThanOrEqualTo(0);
    }
}
