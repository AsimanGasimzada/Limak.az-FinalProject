using FluentValidation;
using Limak.Application.DTOs.ChatDTOs;

namespace Limak.Application.Validators.ChatValidators;

public class ChatPutOperatorDtoValidator : AbstractValidator<ChatPutOperatorDto>
{
    public ChatPutOperatorDtoValidator()
    {
        RuleFor(x => x.Id).NotNull().GreaterThan(0);
        RuleFor(x => x.OperatorId).NotNull().GreaterThan(0);
    }
}
