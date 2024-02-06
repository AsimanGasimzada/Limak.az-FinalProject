using Limak.Application.Abstractions.Utilities;

namespace Limak.Persistence.Utilities.Exceptions.Common;

public class CannotDeleteException:Exception,IBaseException
{
    public CannotDeleteException(string message="this object cannot be deleted"):base(message)
    {
        
    }
}
