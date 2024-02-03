namespace Limak.Persistence.Utilities.Exceptions.Common;

public class NotFoundException : Exception
{
    public NotFoundException(string message = "not found!") : base(message)
    {

    }
}
