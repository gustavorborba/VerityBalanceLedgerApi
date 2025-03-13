using BalanceLedgerApi.Application.Dto;
using BalanceLedgerApi.Domain.Model;

namespace BalanceLedgerApi.Application.Interface.Service
{
    public interface ITransactionService
    {
        Task<CommonResponseDto<TransactionDto>> Save(TransactionDto transactionDto);
        Task<IEnumerable<Transaction>> All();
    }
}
