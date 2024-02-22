using Limak.Application.DTOs.AuthDTOs;
using Limak.Application.DTOs.MessageDTOs;

namespace Limak.Application.DTOs.ChatDTOs;

public class ChatGetDto
{
    public int Id { get; set; }
    public int AppUserId { get; set; }
    public AppUserRelationDto AppUser { get; set; } = null!;
    public int OperatorId { get; set; }
    public AppUserRelationDto Operator { get; set; } = null!;
    public bool IsActive { get; set; }
    public List<MessageRelationDto> Messages { get; set; } = new();
}
