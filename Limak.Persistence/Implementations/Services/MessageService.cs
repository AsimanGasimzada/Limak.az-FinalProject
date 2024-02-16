using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.MessageDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Helpers;

namespace Limak.Persistence.Implementations.Services;

public class MessageService : IMessageService
{

    private readonly IMessageRepository _repository;
    private readonly IMapper _mapper;
    private readonly IChatService _chatService;
    private readonly IAuthService _authService;
    private readonly ICloudinaryService _cloudinaryService;
    public MessageService(IMessageRepository repository, IMapper mapper, IChatService chatService, IAuthService authService, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _mapper = mapper;
        _chatService = chatService;
        _authService = authService;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<ResultDto> SendMessageAsync(MessagePostDto dto)
    {
        var user = await _authService.GetCurrentUserAsync();

        var chat = await _chatService.GetByIdAsync(dto.ChatId);

        if (chat.AppUserId != user.Id && chat.OperatorId != user.Id)
            throw new NotFoundException($"{dto.ChatId}-Chat is not found");

        var message = _mapper.Map<Message>(dto);

        if (dto.File is not null)
        {
            if (!dto.File.ValidateSize(5))
                throw new InvalidInputException("The size of the file cannot be more than 5 MB");

            message.FilePath = await _cloudinaryService.FileCreateAsync(dto.File);
        }

        await _repository.CreateAsync(message);
        await _repository.SaveAsync();

        return new("Message is successfully created");
    }
}
