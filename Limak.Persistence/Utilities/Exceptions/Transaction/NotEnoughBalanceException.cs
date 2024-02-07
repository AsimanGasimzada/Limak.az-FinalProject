using Limak.Application.Abstractions.Utilities;

namespace Limak.Persistence.Utilities.Exceptions.Transaction;

public class NotEnoughBalanceException:Exception,IBaseException
{
    public NotEnoughBalanceException(string message= "there is not enough balance"):base(message)   
    {
        
    }
}
