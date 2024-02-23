namespace Limak.Application.DTOs.SettingDTOs;

public class SettingGetDto
{
    public int Id { get; set; }
    public string Key { get; set; } = null!;
    public string Value { get; set; } = null!;
}
