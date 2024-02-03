using Limak.Application.Abstractions.Utilities;

namespace Limak.Persistence.Utilities.Exceptions.Common;

public class AlreadyExistException:Exception,IBaseException
{
    public AlreadyExistException(string message="This element is already exist"):base(message)
    {
        
    }
}
