using Limak.Application.DTOs.Common;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.StripeDTOs;
using Limak.Application.DTOs.TransactionDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Application.Abstractions.Services;

public interface ITransactionService
{
    Task<ResultDto> IncreaseAZNBalance(StripePayDto dto);
    Task<ResultDto> IncreaseTRYBalance(StripePayDto dto);
    Task<ResultDto> IncreaseUSDBalance(StripePayDto dto);
    Task<ResultDto> PaymentByAZNBalance(BalancePutDto dto);
    Task<ResultDto> PaymentByTRYBalance(BalancePutDto dto);
    Task<ResultDto> PaymentByUSDBalance(BalancePutDto dto);
    Task<GetBalanceDto> GetBalances();
    Task<List<TransactionGetDto>> GetAllTransactions();
    Task<ExportExcelDto> ExportToExcelAsync();
    Task<ResultDto> IncreaseAZNBalanceAdmin(BalanceAdminPutDto dto);
    Task<ResultDto> IncreaseTRYBalanceAdmin(BalanceAdminPutDto dto);
    Task<ResultDto> IncreaseUSDBalanceAdmin(BalanceAdminPutDto dto);


}
