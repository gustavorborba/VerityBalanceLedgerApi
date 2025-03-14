namespace BalanceLedgerApi.Application.Interface.Service
{
    public interface ITransactionService
    {
        Task<CommonResponseDto<TransactionDto>> Save(TransactionDto transactionDto);
        Task<CommonResponseDto<IEnumerable<TransactionDto>>> All();
    }
}
