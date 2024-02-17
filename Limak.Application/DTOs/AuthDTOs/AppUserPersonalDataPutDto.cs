namespace Limak.Application.DTOs.AuthDTOs;

public class AppUserPersonalDataPutDto
{
    public string SeriaNumber { get; set; }
    public string FinCode { get; set; }
    public int CitizenshipId { get; set; }
    public int GenderId { get; set; }
    public string Location { get; set; }

}