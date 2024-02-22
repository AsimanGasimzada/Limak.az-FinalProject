using Limak.Application.DTOs.RequestDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.DTOs.RequestSubjectDTOs;

public class RequestSubjectGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<RequestRelationDto> Requests { get; set; } = new List<RequestRelationDto>();

}
