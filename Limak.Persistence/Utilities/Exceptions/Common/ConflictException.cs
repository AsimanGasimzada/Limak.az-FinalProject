using Limak.Application.Abstractions.Utilities;

namespace Limak.Persistence.Utilities.Exceptions.Common;

public class ConflictException:Exception,IBaseException
{
    public ConflictException(string message = "Conflict!"):base(message)
    {
        
    }
}
