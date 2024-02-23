using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.ChatDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Exceptions.Identity;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class ChatService : IChatService
{
    private readonly IChatRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly INotificationService _notificationService;
    public ChatService(IChatRepository repository, IMapper mapper, INotificationService notificationService, IAuthService authService)
    {
        _repository = repository;
        _mapper = mapper;
        _notificationService = notificationService;
        _authService = authService;
    }

    public async Task<ResultDto> CreateAsync()
    {
        var user = await _authService.GetCurrentUserAsync();

        var existedChats = await _repository.GetFiltered(x => x.AppUserId == user.Id && x.IsActive).ToListAsync();

        foreach (var existedChat in existedChats)
        {
            existedChat.IsActive = false;
            _repository.Update(existedChat);
        }

        Chat chat = new() { AppUserId = user.Id, IsActive = true };

        var moderators = await _authService.GetAllModeratorsAsync();

        foreach (var moderator in moderators)
        {
            await _notificationService.CreateAsync(new() { AppUserId = moderator.Id, Subject = "Yeni online çat", Title = $"{user.Id}-User çata qoşulmuşdur" });
        }

        await _repository.CreateAsync(chat);
        await _repository.SaveAsync();

        return new("Chat is successfully created");
    }

    public async Task<List<ChatGetDto>> GetAll()
    {
        var chats = await _repository.GetAll().ToListAsync();
        var dtos = _mapper.Map<List<ChatGetDto>>(chats);
        return dtos;
    }

    public async Task<ChatGetDto> GetByIdAsync(int id)
    {
        var chat = await _repository.GetSingleAsync(x => x.Id == id);
        if (chat is null)
            throw new NotFoundException($"{id}-This chat is not found");

        var dto = _mapper.Map<ChatGetDto>(chat);
        return dto;
    }

    public async Task<bool> IsExistAsync(int id)
    {
        return await _repository.IsExistAsync(x => x.Id == id);
    }

    public async Task<ResultDto> SetOperatorAsync(int chatId)
    {
        var chat = await _getChatById(chatId);

        var user = await _authService.GetCurrentUserAsync();
        var role = await _authService.GetUserRoleAsync(user.Id);

        if (role is "Member")
            throw new UnAuthorizedException("A member cannot be an operator");

        chat.OperatorId = user.Id;

        _repository.Update(chat);
        await _repository.SaveAsync();

        return new("Operator is successfully setup");
    }



    public async Task<ResultDto> UpdateAsync(ChatPutDto dto)
    {
        var chat = await _getChatById(dto.Id);

        chat.Feedback = dto.Feedback;
        chat.IsActive = false;

        _repository.Update(chat);
        await _repository.SaveAsync();

        return new("Repository is successfully updated");
    }



    public async Task<ChatGetDto> GetOnlineChatAsync()
    {
        var user = await _authService.GetCurrentUserAsync();
        var chat = await _repository.GetSingleAsync(x => x.AppUserId == user.Id, false, "AppUser","Operator","Messages");
        if (chat is null)
        {
            await CreateAsync();
            chat = await _repository.GetSingleAsync(x => x.AppUserId == user.Id);
        }

        var dto = _mapper.Map<ChatGetDto>(chat);
 
        return dto;
    }



    public async Task<List<ChatGetDto>> GetWithoutAnOperatorChats()
    {

        var requests = await _repository.GetFiltered(x => x.OperatorId == 0 || x.OperatorId == null, false, "AppUser", "Operator", "Messages").ToListAsync();

        if (requests.Count is 0)
            throw new NotFoundException("Request is not found");

        var dtos = _mapper.Map<List<ChatGetDto>>(requests);

        return dtos;
    }


    private async Task<Chat> _getChatById(int id)
    {
        var chat = await _repository.GetSingleAsync(x => x.Id == id,false,"Operator","AppUser","Messages");
        if (chat is null)
            throw new NotFoundException($"{id}-Chat is not found");
        return chat;
    }
}
