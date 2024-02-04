using Limak.Application.Abstractions.Utilities;

namespace Limak.Persistence.Utilities.Exceptions.Identity;

public class LoginException:Exception,IBaseException
{
    public LoginException(string message="Email or password is wrong!"):base(message)
    {
        
    }
}
