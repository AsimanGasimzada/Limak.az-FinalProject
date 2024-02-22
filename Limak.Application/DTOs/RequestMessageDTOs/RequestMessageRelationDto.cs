namespace Limak.Application.DTOs.RequestMessageDTOs;

public class RequestMessageRelationDto
{
    public int AppUserId { get; set; }
    public int RequestId { get; set; }
    public string Message { get; set; } = null!;
    public DateTime CreatedTime { get; set; }

}
