using FluentValidation;
using Limak.Application.DTOs.RequestSubjectDTOs;

namespace Limak.Application.Validators.RequestSubjectValidators;

public class RequestSubjectPutDtoValidator : AbstractValidator<RequestSubjectPutDto>
{
    public RequestSubjectPutDtoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Name).NotNull().MaximumLength(128).MinimumLength(3);
    }
}


