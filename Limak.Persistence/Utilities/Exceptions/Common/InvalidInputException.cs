using Limak.Application.Abstractions.Utilities;

namespace Limak.Persistence.Utilities.Exceptions.Common;

public class InvalidInputException : Exception,IBaseException
{
    public InvalidInputException(string message = "Invalid input!") : base(message)
    {

    }
}
