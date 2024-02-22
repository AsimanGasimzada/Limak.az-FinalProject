using Limak.Application.DTOs.AuthDTOs;
using Limak.Application.DTOs.RequestDTOs;

namespace Limak.Application.DTOs.RequestMessageDTOs;

public class RequestMessageGetDto
{
    public int AppUserId { get; set; }
    public AppUserRelationDto AppUser { get; set; } = null!;
    public int RequestId { get; set; }
    public RequestRelationDto Request { get; set; } = null!;
    public string Message { get; set; } = null!;
}
