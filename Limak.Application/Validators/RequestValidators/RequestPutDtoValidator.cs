using FluentValidation;
using Limak.Application.DTOs.RequestDTOs;

namespace Limak.Application.Validators.RequestValidators;

public class RequestPutDtoValidator : AbstractValidator<RequestPutDto>
{
    public RequestPutDtoValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.Status).NotEmpty();
    }
}
