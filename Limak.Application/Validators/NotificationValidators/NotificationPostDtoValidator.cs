using FluentValidation;
using Limak.Application.DTOs.NotificationDTOs;

namespace Limak.Application.Validators.NotificationValidators;

public class NotificationPostDtoValidator : AbstractValidator<NotificationPostDto>
{
    public NotificationPostDtoValidator()
    {
        RuleFor(x => x.Subject).NotNull().MaximumLength(128).MinimumLength(3);
        RuleFor(x => x.Title).NotNull().MaximumLength(1024).MinimumLength(3);
        RuleFor(x => x.AppUserId).NotNull().GreaterThan(0);
    }
}
