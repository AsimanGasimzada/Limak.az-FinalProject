using Limak.Application.Abstractions.Utilities;

namespace Limak.Persistence.Utilities.Exceptions.Common;

public class NotFoundException : Exception,IBaseException
{
    public NotFoundException(string message = "not found!") : base(message)
    {

    }
}
