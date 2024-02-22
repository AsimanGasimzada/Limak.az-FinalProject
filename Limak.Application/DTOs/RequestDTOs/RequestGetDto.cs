using Limak.Application.DTOs.AuthDTOs;
using Limak.Application.DTOs.CountryDTOs;
using Limak.Application.DTOs.RequestMessageDTOs;
using Limak.Application.DTOs.RequestSubjectDTOs;

namespace Limak.Application.DTOs.RequestDTOs;

public class RequestGetDto
{
    public int Id { get; set; }
    public int CountryId { get; set; }
    public CountryGetDto Country { get; set; } = null!;
    public int RequestSubjectId { get; set; }
    public RequestSubjectRelationDto RequestSubject { get; set; }=null!;
    public bool? Status { get; set; }
    public int AppUserId { get; set; }
    public AppUserRelationDto AppUser { get; set; } = null!;
    public int? OperatorId { get; set; }
    public AppUserRelationDto Operator { get; set; } = null!;
    public List<RequestMessageRelationDto> RequestMessages { get; set; } = new List<RequestMessageRelationDto>();

}
