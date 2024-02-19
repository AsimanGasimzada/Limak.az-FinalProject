using FluentValidation;
using Limak.Application.DTOs.RequestMessageDTOs;

namespace Limak.Application.Validators.RequestMessageValidators;

public class RequestPostDtoValidator : AbstractValidator<RequestMessagePostDto>
{
    public RequestPostDtoValidator()
    {
        RuleFor(x => x.RequestId).NotNull().GreaterThan(0);
        RuleFor(x => x.Message).NotNull().MaximumLength(4096);
    }
}
