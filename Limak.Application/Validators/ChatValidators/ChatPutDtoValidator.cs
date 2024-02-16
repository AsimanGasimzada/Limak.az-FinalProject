using FluentValidation;
using Limak.Application.DTOs.ChatDTOs;

namespace Limak.Application.Validators.ChatValidators;

public class ChatPutDtoValidator:AbstractValidator<ChatPutDto>
{
    public ChatPutDtoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.Feedback).NotNull().MaximumLength(2048);
    }
}



public class ChatPutOperatorDtoValidator : AbstractValidator<ChatPutOperatorDto>
{
    public ChatPutOperatorDtoValidator()
    {
        RuleFor(x => x.Id).NotNull();
        RuleFor(x => x.OperatorId).NotNull();
    }
}
