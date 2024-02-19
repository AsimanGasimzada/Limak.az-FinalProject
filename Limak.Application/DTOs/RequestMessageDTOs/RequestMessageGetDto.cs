namespace Limak.Application.DTOs.RequestMessageDTOs;

public class RequestMessageGetDto
{
    public int AppUserId { get; set; }
    public int RequestId { get; set; }
    public string Message { get; set; } = null!;
}
