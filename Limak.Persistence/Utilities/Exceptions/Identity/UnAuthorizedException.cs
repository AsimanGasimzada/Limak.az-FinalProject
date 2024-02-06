using Limak.Application.Abstractions.Utilities;

namespace Limak.Persistence.Utilities.Exceptions.Identity;

public class UnAuthorizedException:Exception,IBaseException
{
    public UnAuthorizedException(string message= "UnAuthorized 401!"):base(message)
    {
        
    }
}
