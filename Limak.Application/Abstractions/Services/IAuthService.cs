using Limak.Application.DTOs.AuthDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.Abstractions.Services;

public interface IAuthService
{
    Task<ResultDto> RegisterAsync(RegisterDto dto);
    Task<AccessToken> LoginAsync(LoginDto dto);
    Task<AccessToken> ConfirmEmailAsync(ConfirmEmailDto dto);
    Task<AccessToken> RefreshToken(string refreshToken);
    Task<ResultDto> CreateRolesAsync();
    Task<AccessToken> ChangePasswordAsync(ChangePasswordDto dto);
    Task<AppUserGetDto> GetCurrentUserAsync();
    Task<ResultDto> SendForgetPasswordMail(ForgetPasswordDto dto);
    Task<AppUserGetDto> CheckResetPasswordToken(ForgetPasswordTokenDto dto);
    Task<AccessToken> ResetPasswordAsync(ResetPasswordTokenDto dto);
}
