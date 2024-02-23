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
    Task<AccessToken> ChangePasswordAsync(ChangePasswordDto dto);
    Task<AppUserGetDto> GetCurrentUserAsync();
    Task<ResultDto> SendForgetPasswordMail(ForgetPasswordDto dto);
    Task<AccessToken> ResetPasswordAsync(ResetPasswordTokenDto dto);
    Task<List<AppUserGetDto>> GetAllModeratorsAsync();

    Task<ResultDto> EditUserAccountDatas(AppUserAccountDataPutDto dto);
    Task<ResultDto> EditUserPersonalDatas(AppUserPersonalDataPutDto dto);
    Task<AccessToken> ChangeEmailAsync(ChangeEmailDto dto);
    Task<string> GetUserRoleAsync(int AppUserId);
    Task<ResultDto> ChangeUserRoleAsync(ChangeRoleDto dto);
    Task<AppUserGetDto> GetUserByIdAsync(int id);
    Task<AppUserGetDto> GetUserByUsernameAsync(string userName);
    Task<List<AppUserGetDto>> GetAllUsersAsync(string? search);
    Task<AppUserGetDto> CheckResetPasswordToken(ForgetPasswordTokenDto dto);
    Task<ResultDto> ChangePasswordByAdminAsync(ChangePasswordByAdminDto dto);
    Task<AppUserGetDto> FindByFincode(string Fincode);
}
