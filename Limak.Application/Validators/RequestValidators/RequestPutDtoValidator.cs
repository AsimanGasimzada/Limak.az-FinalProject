using FluentValidation;
using Limak.Application.DTOs.RequestDTOs;

namespace Limak.Application.Validators.RequestValidators;

public class RequestPutDtoValidator : AbstractValidator<RequestPutDto>
{
    public RequestPutDtoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Status).NotEmpty();
    }
}
