using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.MessageDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Implementations.Hubs;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Helpers;
using Microsoft.AspNetCore.SignalR;

namespace Limak.Persistence.Implementations.Services;

public class MessageService : IMessageService
{

    private readonly IMessageRepository _repository;
    private readonly IMapper _mapper;
    private readonly IChatService _chatService;
    private readonly IHubContext<ChatHub> _chatHub;
    private readonly IAuthService _authService;
    private readonly ICloudinaryService _cloudinaryService;
    public MessageService(IMessageRepository repository, IMapper mapper, IChatService chatService, IAuthService authService, ICloudinaryService cloudinaryService, IHubContext<ChatHub> chatHub)
    {
        _repository = repository;
        _mapper = mapper;
        _chatService = chatService;
        _authService = authService;
        _cloudinaryService = cloudinaryService;
        _chatHub = chatHub;
    }

    public async Task<ResultDto> SendMessageAsync(MessagePostDto dto)
    {
        var user = await _authService.GetCurrentUserAsync();

        var chat = await _chatService.GetByIdAsync(dto.ChatId);

        if (chat.AppUserId != user.Id && chat.OperatorId != user.Id)
            throw new NotFoundException($"{dto.ChatId}-Chat is not found");

        var message = _mapper.Map<Message>(dto);
        message.AppUserId = user.Id;
        if (dto.File is not null)
        {
            dto.File.ValidateImage(5); // cloudinary service başqa fayllar dəstəkləmir :(   
            message.FilePath = await _cloudinaryService.FileCreateAsync(dto.File);
        }

        await _repository.CreateAsync(message);
        await _repository.SaveAsync();

        var userConnectionIds = ChatHub.Connections.FirstOrDefault(x => x.UserId == chat.AppUserId)?.ConnectionIds;
        if (userConnectionIds is not null)
        {
            foreach (var connectionId in userConnectionIds)
            {
                await _chatHub.Clients.Client(connectionId).SendAsync("ReceiveChatMessage", dto);
            }
        }

        var MemberConnectionIds = ChatHub.Connections.FirstOrDefault(x => x.UserId == chat.OperatorId)?.ConnectionIds;
        if (MemberConnectionIds is not null)
        {
            foreach (var connectionId in MemberConnectionIds)
            {
                await _chatHub.Clients.Client(connectionId).SendAsync("ReceiveChatMessage", dto);
            }
        }


        return new("Message is successfully created");
    }
}
