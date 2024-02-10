using FluentValidation;
using Limak.Application.DTOs.TransactionDTOs;

namespace Limak.Application.Validators.TransactionValidators;

public class BalanceAdminPutDtoValidator : AbstractValidator<BalanceAdminPutDto>
{
    public BalanceAdminPutDtoValidator()
    {
        RuleFor(x => x.AppUserId).NotEmpty().GreaterThan(0);
        RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
    }
}
