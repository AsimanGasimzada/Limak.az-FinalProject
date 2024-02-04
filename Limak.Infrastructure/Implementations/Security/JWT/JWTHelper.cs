using Limak.Application.Abstractions.Helpers;
using Limak.Application.DTOs.AuthDTOs;
using Limak.Domain.Entities;
using Limak.Infrastructure.Implementations.Security.Encrypting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Limak.Infrastructure.Implementations.Security.JWT;

public class JWTHelper : ITokenHelper
{
    private readonly IConfiguration _configuration;
    private readonly TokenOptionDto _tokenOptions;
    private readonly DateTime _expiresAt;
    public JWTHelper(IConfiguration configuration)
    {
        _configuration = configuration;
        _tokenOptions = _configuration.GetSection("TokenOptions").Get<TokenOptionDto>();
        _expiresAt = DateTime.UtcNow.AddMinutes(_tokenOptions.TokenExpiration);
    }

    public AccessToken CreateToken(List<Claim> claims)
    {
        JwtHeader jwtHeader = CreateJwtHeader();
        JwtPayload jwtPayload = CreateJwtPayload(claims);
        JwtSecurityToken jwtToken = new(jwtHeader, jwtPayload);

        return CreateAccessToken(jwtToken);
        
    }

    private AccessToken CreateAccessToken(JwtSecurityToken jwtToken)
    {
        JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
        return new()
        {
            Token = jwtSecurityTokenHandler.WriteToken(jwtToken),
            ExpiredDate = _expiresAt,
            RefreshToken = GenerateRefreshToken(),
            RefreshTokenExpiredAt = _expiresAt.AddMinutes(15)
        };

    }

    private JwtPayload CreateJwtPayload(List<Claim> claims)
    {
        return new(
            issuer: _tokenOptions.Issuer,
            audience: _tokenOptions.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: _expiresAt
            );
    }

    private JwtHeader CreateJwtHeader()
    {
        SecurityKey securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
        SigningCredentials signingCredentials = SigninCredentialsHelper.CreateSigninCredentials(securityKey);
        JwtHeader jwtHeader = new(signingCredentials);
        return jwtHeader;
    }

    private string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }
}
