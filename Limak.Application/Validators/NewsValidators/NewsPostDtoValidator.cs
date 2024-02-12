using FluentValidation;
using Limak.Application.DTOs.NewsDTOs;

namespace Limak.Application.Validators.NewsValidators;

public class NewsPostDtoValidator : AbstractValidator<NewsPostDto>
{
    public NewsPostDtoValidator()
    {
        RuleFor(x => x.Subject).NotNull().MinimumLength(3).MaximumLength(128);
        RuleFor(x => x.Description).NotNull().MinimumLength(3).MaximumLength(4098);
        RuleFor(x => x.Image).NotNull();
    }
}
