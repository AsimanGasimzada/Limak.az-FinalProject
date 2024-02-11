using Limak.Application.DTOs.AuthDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.Abstractions.Services;

public interface IAuthService
{
    Task<AccessToken> RegisterAsync(RegisterDto dto);
    Task<AccessToken> LoginAsync(LoginDto dto);
    Task<AccessToken> ConfirmEmailAsync(string token);
    Task<AccessToken> RefreshToken(string refreshToken);
    Task<ResultDto> CreateRolesAsync();
    Task<AccessToken> ChangePasswordAsync(ChangePasswordDto dto);
    Task<AppUserGetDto> GetCurrentUserAsync();
}
