using Limak.Domain.Entities;
using System.Security.Claims;

namespace Limak.Application.Abstractions.Helpers;

public interface ITokenHelper
{
    AccessToken CreateToken(List<Claim> claims);

}
