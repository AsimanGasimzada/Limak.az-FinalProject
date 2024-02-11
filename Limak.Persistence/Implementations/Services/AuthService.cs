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

namespace Limak.Persistence.Implementations.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;
    private readonly ITokenHelper _tokenHelper;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _accessor;
    private readonly IEmailHelper _emailHelper;
    public AuthService(UserManager<AppUser> userManager, IMapper mapper, RoleManager<IdentityRole<int>> roleManager, ITokenHelper tokenHelper, IHttpContextAccessor accessor, IEmailHelper emailHelper)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleManager = roleManager;
        _tokenHelper = tokenHelper;
        _accessor = accessor;
        _emailHelper = emailHelper;
    }

    public async Task<AccessToken> RegisterAsync(RegisterDto dto)
    {
        var isExistSerialNumber = await _userManager.Users.AnyAsync(x => x.SeriaNumber == dto.SeriaNumber);
        if (isExistSerialNumber)
            throw new ConflictException("This Serial Number also exists in the user");

        var isExistFincode = await _userManager.Users.AnyAsync(x => x.FinCode == dto.FinCode);
        if (isExistFincode)
            throw new ConflictException("This Fincode also exists in the user");

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

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        await _emailHelper.SendEmailAsync(new()
        {
            Subject = "Confirm Email",
            Body = token,
            ToEmail = user.Email
        });


        return accessToken;
    }
    public async Task<ResultDto> CreateRolesAsync()
    {
        foreach (var role in Enum.GetNames(typeof(IdentityRoles)))
        {
            await _roleManager.CreateAsync(new() { Name = role });
        }
        return new("Successfully Created");
    }
    private async Task<List<Claim>> ClaimsCreateAsync(AppUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>() {

            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim("PhoneNumber",user.PhoneNumber),
            new Claim("FinCode",user.FinCode),
            new Claim("SerialNumber",user.SeriaNumber),
            new Claim(ClaimTypes.Role, roles.FirstOrDefault())

        };


        return claims;
    }

    public async Task<AccessToken> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user is null)
            throw new LoginException();

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
        var id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            throw new NotFoundException("User is not found!");



        var result = await _userManager.ChangePasswordAsync(user, dto.ExistPassword, dto.NewPassword);
        if (!result.Succeeded)
            throw new InvalidInputException(string.Join(" ", result.Errors.Select(e => e.Description)));

        var accessToken = await CreateAccessToken(user);

        return accessToken;
    }
    private async Task<AccessToken> CreateAccessToken(AppUser? user)
    {
        var claims = (await _userManager.GetClaimsAsync(user)).ToList();
        var accessToken = _tokenHelper.CreateToken(claims);
        user.RefreshToken = accessToken.RefreshToken;
        user.RefreshTokenExpiredAt = accessToken.RefreshTokenExpiredAt;
        await _userManager.UpdateAsync(user);
        return accessToken;
    }


    public async Task<AccessToken> ConfirmEmailAsync(string token)
    {
        var id = _accessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            throw new NotFoundException("User is not found!");


        var result = await _userManager.ConfirmEmailAsync(user, token);

        if (!result.Succeeded)
            throw new InvalidInputException(string.Join(" ", result.Errors.Select(e => e.Description)));


        return await CreateAccessToken(user);
    }

    public async Task<AppUserGetDto> GetCurrentUserAsync()
    {
        var id = _accessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (id is null)
            throw new UnAuthorizedException();

        var user =await _userManager.FindByIdAsync(id);
        if (user is null)
            throw new UnAuthorizedException();

        var dto = _mapper.Map<AppUserGetDto>(user);

        return dto;
    }
}
