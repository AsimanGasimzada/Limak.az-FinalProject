using AutoMapper;
using Limak.Application.Abstractions.Helpers;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.AuthDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Domain.Enums;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Exceptions.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using System.Text;

namespace Limak.Persistence.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly ITokenHelper _tokenHelper;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _accessor;
    private readonly IEmailHelper _emailHelper;
    private readonly ICitizenshipService _citizenshipService;
    private readonly IUserPositionService _userPositionService;
    private readonly IWarehouseService _warehouseService;
    private readonly IGenderService _genderService;

    public AuthService(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole<int>> roleManager, ITokenHelper tokenHelper, IHttpContextAccessor accessor, IEmailHelper emailHelper, ICitizenshipService citizenshipService, IUserPositionService userPositionService, IWarehouseService warehouseService, IGenderService genderService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
        _tokenHelper = tokenHelper;
        _accessor = accessor;
        _emailHelper = emailHelper;
        _citizenshipService = citizenshipService;
        _userPositionService = userPositionService;
        _warehouseService = warehouseService;
        _genderService = genderService;
    }

    public async Task<ResultDto> RegisterAsync(RegisterDto dto)
    {
        var isExistSerialNumber = await _userManager.Users.AnyAsync(x => x.SeriaNumber == dto.SeriaNumber);
        if (isExistSerialNumber)
            throw new ConflictException($"{dto.SeriaNumber}-This Serial Number also exists in the user");

        var isExistFincode = await _userManager.Users.AnyAsync(x => x.FinCode == dto.FinCode);
        if (isExistFincode)
            throw new ConflictException($"{dto.FinCode}-This Fincode also exists in the user");

        var isExistUserPosition = await _userPositionService.IsExist(dto.UserPositionId);
        if (!isExistUserPosition)
            throw new NotFoundException($"{dto.UserPositionId}-this User position is not found");

        var isExistCitizenship = await _citizenshipService.IsExist(dto.CitizenshipId);
        if (!isExistCitizenship)
            throw new NotFoundException($"{dto.CitizenshipId}-this Citizenship is not found");

        var isExistWarehouseId = await _warehouseService.IsExist(dto.WarehouseId);
        if (!isExistWarehouseId)
            throw new NotFoundException($"{dto.WarehouseId}-this Warehouse is not found");

        var isExistGender = await _genderService.IsExist(dto.GenderId);
        if (!isExistCitizenship)
            throw new NotFoundException($"{dto.GenderId}-this Gender is not found");


        var user = _mapper.Map<AppUser>(dto);
        user.UserName = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            throw new InvalidInputException(string.Join(" ", result.Errors.Select(e => e.Description)));

        await _userManager.AddToRoleAsync(user, IdentityRoles.Member.ToString());

        List<Claim> Claims = await ClaimsCreateAsync(user);

        await _userManager.AddClaimsAsync(user, Claims);

        var accessToken = _tokenHelper.CreateToken(Claims);
        user.RefreshToken = accessToken.RefreshToken;
        user.RefreshTokenExpiredAt = accessToken.RefreshTokenExpiredAt;
        await _userManager.UpdateAsync(user);


        await SendEmailConfirmRequest(user);


        return new($"The user has been successfully created, please check your email inbox for email confirmation.");
    }
    public async Task<AccessToken> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
            throw new LoginException();
        if (!user.EmailConfirmed)
            throw new LoginException("User is email not confirmed please check your email inbox");


        var result = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (user.AccessFailedCount == 3)
        {
            if (user.LockoutEnd > DateTime.UtcNow)
                throw new LoginException("The user is blocked and try again after 5 minutes");
            user.LockoutEnabled = false;
            user.AccessFailedCount = 0;
            user.LockoutEnd = null;
            await _userManager.UpdateAsync(user);
        }
        if (!result)
        {
            user.AccessFailedCount++;
            if (user.AccessFailedCount == 3)
            {
                user.LockoutEnabled = true;
                user.LockoutEnd = DateTime.UtcNow.AddMinutes(5);
            }
            await _userManager.UpdateAsync(user);
            throw new LoginException();
        }
        user.AccessFailedCount = 0;
        await _userManager.UpdateAsync(user);
        //SignInManager işləmədiyi üçün custom Lockout yazdım

        var claims = (await _userManager.GetClaimsAsync(user)).ToList();
        var accessToken = _tokenHelper.CreateToken(claims);
        user.RefreshToken = accessToken.RefreshToken;
        user.RefreshTokenExpiredAt = accessToken.RefreshTokenExpiredAt;
        await _userManager.UpdateAsync(user);

        return accessToken;
    }
    public async Task<string> GetUserRoleAsync(int AppUserId)
    {
        var user = await _getUserById(AppUserId.ToString());
        var roles = await _userManager.GetRolesAsync(user);
        return roles.FirstOrDefault() ?? "null";
    }
    public async Task<AccessToken> RefreshToken(string refreshToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshToken == refreshToken);
        if (user is null)
            throw new NotFoundException("User is not found!");

        if (user.RefreshTokenExpiredAt < DateTime.UtcNow)
            throw new LoginException("Token is expired at");

        AccessToken accessToken = await CreateAccessToken(user);

        return accessToken;
    }
    public async Task<AccessToken> ChangePasswordAsync(ChangePasswordDto dto)
    {
        var id = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            throw new NotFoundException("User is not found!");



        var result = await _userManager.ChangePasswordAsync(user, dto.ExistPassword, dto.NewPassword);
        if (!result.Succeeded)
            throw new InvalidInputException(string.Join(" ", result.Errors.Select(e => e.Description)));

        var accessToken = await CreateAccessToken(user);

        return accessToken;
    }
    public async Task<AccessToken> ConfirmEmailAsync(ConfirmEmailDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.AppUserId);
        if (user is null)
            throw new NotFoundException("User is not found!");


        var result = await _userManager.ConfirmEmailAsync(user, dto.Token);

        if (!result.Succeeded)
            throw new InvalidInputException(string.Join(" ", result.Errors.Select(e => e.Description)));


        return await CreateAccessToken(user);
    }
    public async Task<AppUserGetDto> GetCurrentUserAsync()
    {
        var id = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (id is null)
            throw new UnAuthorizedException();

        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            throw new UnAuthorizedException();

        var dto = _mapper.Map<AppUserGetDto>(user);

        dto.Role = await GetUserRoleAsync(dto.Id);

        return dto;
    }
    public async Task<ResultDto> SendForgetPasswordMail(ForgetPasswordDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
            throw new NotFoundException($"{dto.Email}-this user is not found!");


        string token = await _userManager.GeneratePasswordResetTokenAsync(user);

        string path = Path.Combine("http://localhost:3000", $"ForgetPassword?AppUserId={user.Id}&token={token}");
        string body = _resetPasswordBody.Replace("{Replace_Link_1}", path);
        body = body.Replace("{Replace_Link_2}", path);
        body = body.Replace("{Replace_Link_3}", path);


        await _emailHelper.SendEmailAsync(new() { ToEmail = user.Email, Subject = "Limak.az Şifrə yeniləmə", Body = body });

        return new("The password reset link has been successfully sent to your email");
    }
    public async Task<AppUserGetDto> GetUserByIdAsync(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
            throw new NotFoundException("User is not found");

        var dto = _mapper.Map<AppUserGetDto>(user);

        return dto;
    }
    public async Task<List<AppUserGetDto>> GetAllUsersAsync(string? search)
    {
        var users = await _userManager.Users.Where(x => !string.IsNullOrWhiteSpace(search) ? x.UserName.Contains(search) : true).ToListAsync();
        var dtos = _mapper.Map<List<AppUserGetDto>>(users);

        foreach (var dto in dtos)
            dto.Role = await GetUserRoleAsync(dto.Id);

        return dtos;
    }
    public async Task<List<AppUserGetDto>> GetAllModeratorsAsync()
    {
        var users = await _userManager.GetUsersInRoleAsync(IdentityRoles.Moderator.ToString());
        var dtos = _mapper.Map<List<AppUserGetDto>>(users);
        return dtos;
    }
    public async Task<AppUserGetDto> CheckResetPasswordToken(ForgetPasswordTokenDto dto)
    {
        var user = await _getUserById(dto.AppUserId);

        var userDto = _mapper.Map<AppUserGetDto>(user);

        return userDto;
    }
    public async Task<AccessToken> ResetPasswordAsync(ResetPasswordTokenDto dto)
    {
        var user = await _getUserById(dto.AppUserId);

        var result = await _userManager.ResetPasswordAsync(user, dto.Token, dto.Password);
        if (!result.Succeeded)
            throw new InvalidInputException(string.Join(" ", result.Errors.Select(e => e.Description)));


        return await CreateAccessToken(user);
    }
    public async Task<ResultDto> EditUserAccountDatas(AppUserAccountDataPutDto dto)
    {

        var isExistWarehouseId = await _warehouseService.IsExist(dto.WarehouseId);
        if (!isExistWarehouseId)
            throw new NotFoundException($"{dto.WarehouseId}-this Warehouse is not found");


        var userDto = await GetCurrentUserAsync();

        var user = await _getUserById(userDto.Id.ToString());


        user = _mapper.Map(dto, user);

        if (userDto.Email != dto.Email)
        {
            await SendEmailChangeRequest(user, dto.Email);
            user.EmailConfirmed = false;
        }

        await _userManager.UpdateAsync(user);

        if (userDto.Email != dto.Email)
            return new("To verify your email, click on the link sent to your email address");

        return new("User successfully updated");

    }
    public async Task<AccessToken> ChangeEmailAsync(ChangeEmailDto dto)
    {
        var user = await _getUserById(dto.Id.ToString());
        var currentUser = await GetCurrentUserAsync();
        if (user.Id != currentUser.Id)
            throw new UnAuthorizedException();

        var result = await _userManager.ChangeEmailAsync(user, dto.Email, dto.Token);

        if (!result.Succeeded)
            throw new InvalidInputException(string.Join(" ", result.Errors.Select(e => e.Description)));

        return await CreateAccessToken(user);
    }
    public async Task<ResultDto> EditUserPersonalDatas(AppUserPersonalDataPutDto dto)
    {

        var userDto = await GetCurrentUserAsync();
        var user = await _getUserById(userDto.Id.ToString());

        var isExistSerialNumber = await _userManager.Users.AnyAsync(x => x.SeriaNumber == dto.SeriaNumber && x.Id != user.Id);
        if (isExistSerialNumber)
            throw new ConflictException($"{dto.SeriaNumber}-This Serial Number also exists in the user");

        var isExistFincode = await _userManager.Users.AnyAsync(x => x.FinCode == dto.FinCode && x.Id != user.Id);
        if (isExistFincode)
            throw new ConflictException($"{dto.FinCode}-This Fincode also exists in the user");

        var isExistCitizenship = await _citizenshipService.IsExist(dto.CitizenshipId);
        if (!isExistCitizenship)
            throw new NotFoundException($"{dto.CitizenshipId}-this Citizenship is not found");

        var isExistGender = await _genderService.IsExist(dto.GenderId);
        if (!isExistCitizenship)
            throw new NotFoundException($"{dto.GenderId}-this Gender is not found");


        user = _mapper.Map(dto, user);

        await _userManager.UpdateAsync(user);

        return new("User successfully updated");

    }


    public async Task<ResultDto> ChangeUserRoleAsync(ChangeRoleDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.AppUserId.ToString());
        if (user is null)
            throw new NotFoundException($"{dto.AppUserId}-this user is not found");

        var role = await _roleManager.FindByNameAsync(dto.Role);
        if (role is null)
            throw new NotFoundException($"{dto.Role}-this role is not found");
        var existRole = await GetUserRoleAsync(dto.AppUserId);

        var existClaims = await _userManager.GetClaimsAsync(user);

        var result = await _userManager.AddToRoleAsync(user, role.Name);
        if (!result.Succeeded)
            throw new InvalidInputException();
        result = await _userManager.RemoveFromRoleAsync(user, existRole);
        if (!result.Succeeded)
            throw new InvalidInputException();


        await _userManager.RemoveClaimsAsync(user, existClaims);

        var newClaims = await ClaimsCreateAsync(user);
        await _userManager.AddClaimsAsync(user, newClaims);

        return new($"{user.UserName}-User's role is successfully changed");
    }
    public async Task<AppUserGetDto> GetUserByUsernameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
            throw new NotFoundException($"{userName}-User is not found!");

        var dto = _mapper.Map<AppUserGetDto>(user);
        return dto;
    }
    public async Task<ResultDto> ChangePasswordByAdminAsync(ChangePasswordByAdminDto dto)
    {
        var user = await _getUserById(dto.AppUserId.ToString());

        var result = await _userManager.RemovePasswordAsync(user);
        if (!result.Succeeded)
            throw new InvalidInputException(string.Join(" ", result.Errors.Select(e => e.Description)));


        result = await _userManager.AddPasswordAsync(user, dto.NewPassword);

        if (!result.Succeeded)
            throw new InvalidInputException(string.Join(" ", result.Errors.Select(e => e.Description)));

        return new($"{user.Id}-User's password is successfully changed");

    }
    public async Task<AppUserGetDto> FindByFincode(string fincode)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.FinCode.ToLower() == fincode.ToLower());

        if (user is null)
            throw new NotFoundException($"{fincode}-User is not found");


        var dto = _mapper.Map<AppUserGetDto>(user);

        return dto;
    }
    private async Task<AppUser> _getUserById(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        if (user is null)
            throw new NotFoundException("This user is not found");
        return user;
    }
    private async Task<AccessToken> CreateAccessToken(AppUser user)
    {
        var claims = (await _userManager.GetClaimsAsync(user)).ToList();
        var accessToken = _tokenHelper.CreateToken(claims);
        user.RefreshToken = accessToken.RefreshToken;
        user.RefreshTokenExpiredAt = accessToken.RefreshTokenExpiredAt;
        await _userManager.UpdateAsync(user);
        return accessToken;
    }
    private async Task SendEmailConfirmRequest(AppUser user)
    {
        string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        string path = Path.Combine("http://localhost:3000", $"ConfirmEmail?AppUserId={user.Id}&token={token}");
        string body = _confirmEmailBody.Replace("{Replace_Link_1}", path);
        body = body.Replace("{Replace_Link_2}", path);
        body = body.Replace("{Replace_Link_3}", path);


        await _emailHelper.SendEmailAsync(new() { ToEmail = user.Email, Subject = "Limak.az Email Təsdiqləmə", Body = body });
    }
    private async Task SendEmailChangeRequest(AppUser user, string email)
    {
        string token = await _userManager.GenerateChangeEmailTokenAsync(user, email); //bu setirde token yaradıb emaili change edir ona görə çağırdığım yerdə emailConfirm ı false vermişəm
        string path = Path.Combine("http://localhost:3000", $"ChangeEmail?AppUserId={user.Id}&token={token}&email={email}");
        string body = _confirmEmailBody.Replace("{Replace_Link_1}", path);
        body = body.Replace("{Replace_Link_2}", path);
        body = body.Replace("{Replace_Link_3}", path);


        await _emailHelper.SendEmailAsync(new() { ToEmail = email, Subject = "Limak.az Email Yeniləmə", Body = body });
    }
    private async Task<List<Claim>> ClaimsCreateAsync(AppUser user)
    {
        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
        var claims = new List<Claim>() {

            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim("PhoneNumber",user.PhoneNumber),
            new Claim("FinCode",user.FinCode),
            new Claim("SerialNumber",user.SeriaNumber),
            new Claim(ClaimTypes.Role, role?.ToString() ?? "")

        };


        return claims;
    }





    private string _resetPasswordBody = "<!DOCTYPE html>\r\n<html lang=\"az\">\r\n<head>\r\n<meta charset=\"UTF-8\">\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n<title>Şifrəni Sıfırla</title>\r\n</head>\r\n<body style=\"font-family: 'Roboto', sans-serif; background-color: #f4f4f4; padding: 20px; font-weight: 600;\">\r\n\r\n<div style=\"max-width: 600px; margin: 0 auto; background-color: #fff; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0,0,0,0.1);\">\r\n    <h2 style=\"text-align: center; color: #D18337; font-weight: bold;\">Şifrəni Sıfırla</h2>\r\n    <p style=\"color: #555; text-align: justify;\">Salam,</p>\r\n    <p style=\"color: #555; text-align: justify;\">Hesabınızın şifrəsini sıfırlamaq üçün aşağıdakı düyməyə basın:</p>\r\n    <div style=\"text-align: center; margin-top: 20px;\">\r\n        <a href=\"{Replace_Link_1}\" style=\"display: inline-block; background-color: #D18337; color: #fff; text-decoration: none; padding: 10px 20px; border-radius: 5px; font-weight: bold;\">Şifrəni Sıfırla</a>\r\n    </div>\r\n    <p style=\"color: #555; text-align: justify; margin-top: 20px;\">Yuxarıdakı düyməyə basmaqda problemlə qarşılaşırsınızsa, aşağıdakı linki brauzerinizin ünvan panelinə yapışdırın:</p>\r\n    <p style=\"color: #555; text-align: center;\"><a href=\"{Replace_Link_2}\" style=\"color: #D18337; text-decoration: none; font-weight: bold;\">{Replace_Link_3}</a></p>\r\n    <p style=\"color: #555; text-align: justify; margin-top: 20px;\">Əgər siz bu sorğunu etməmisinizsə, lütfən diqqət etməyin.</p>\r\n    <p style=\"color: #555; text-align: justify;\">Sağ olun!</p>\r\n    <p style=\"color: #D18337; text-align: justify;\">Bu e-poçt Limak.az tərəfindən göndərilmişdir.</p>\r\n</div>\r\n\r\n</body>\r\n</html>\r\n";
    private string _confirmEmailBody = "<!DOCTYPE html>\r\n<html lang=\"az\">\r\n<head>\r\n<meta charset=\"UTF-8\">\r\n<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n<title>Email Təsdiqi</title>\r\n</head>\r\n<body style=\"font-family: 'Roboto', sans-serif; background-color: #f4f4f4; padding: 20px; font-weight: 600;\">\r\n\r\n<div style=\"max-width: 600px; margin: 0 auto; background-color: #fff; padding: 20px; border-radius: 10px; box-shadow: 0 0 10px rgba(0,0,0,0.1);\">\r\n    <h2 style=\"text-align: center; color: #D18337; font-weight: bold;\">E-poçt Təsdiqi</h2>\r\n    <p style=\"color: #555; text-align: justify;\">Salam,</p>\r\n    <p style=\"color: #555; text-align: justify;\">E-poçt ünvanınızı təsdiq etmək üçün aşağıdakı düyməyə basın:</p>\r\n    <div style=\"text-align: center; margin-top: 20px;\">\r\n        <a href=\"{Replace_Link_1}\" style=\"display: inline-block; background-color: #D18337; color: #fff; text-decoration: none; padding: 10px 20px; border-radius: 5px; font-weight: bold;\">E-poçt Ünvanımı Təsdiq Et</a>\r\n    </div>\r\n    <p style=\"color: #555; text-align: justify; margin-top: 20px;\">Yuxarıdakı düyməyə basmaqda problemlə qarşılaşırsınızsa, aşağıdakı linki brauzerinizin ünvan panelinə yapışdırın:</p>\r\n    <p style=\"color: #555; text-align: center;\"><a href=\"{Replace_Link_2}\" style=\"color: #D18337; text-decoration: none; font-weight: bold;\">{Replace_Link_3}</a></p>\r\n    <p style=\"color: #555; text-align: justify; margin-top: 20px;\">Əgər bu e-poçtu gözləmirdinizsə, lütfən diqqət etməyin.</p>\r\n    <p style=\"color: #555; text-align: justify;\">Sağ olun!</p>\r\n    <p style=\"color: #D18337; text-align: justify;\">Bu e-poçt Limak.az tərəfindən göndərilmişdir.</p>\r\n</div>\r\n\r\n</body>\r\n</html>\r\n";
}
