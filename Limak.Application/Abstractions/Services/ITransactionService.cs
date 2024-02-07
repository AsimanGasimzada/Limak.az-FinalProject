using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.TransactionDTOs;

namespace Limak.Application.Abstractions.Services;

public interface ITransactionService
{
    Task<ResultDto> IncreaseAZNBalance(BalancePutDto dto);
    Task<ResultDto> IncreaseTRYBalance(BalancePutDto dto);
    Task<ResultDto> IncreaseUSDBalance(BalancePutDto dto);
    Task<ResultDto> PaymentByAZNBalance(BalancePutDto dto);
    Task<ResultDto> PaymentByTRYBalance(BalancePutDto dto);
    Task<ResultDto> PaymentByUSDBalance(BalancePutDto dto);
    
    Task<GetBalanceDto> GetBalances();

}
