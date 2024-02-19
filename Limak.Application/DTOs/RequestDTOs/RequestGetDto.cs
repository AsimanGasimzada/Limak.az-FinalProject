namespace Limak.Application.DTOs.RequestDTOs;

public class RequestGetDto
{
    public int Id { get; set; }
    public int CountryId { get; set; }
    public int RequestSubjectId { get; set; }
    public bool? Status { get; set; }
    public int AppUserId { get; set; }
    public int? OperatorId { get; set; }

}
