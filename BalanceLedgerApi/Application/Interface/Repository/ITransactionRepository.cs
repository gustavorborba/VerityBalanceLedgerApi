namespace BalanceLedgerApi.Application.Interface.Repository
{
    public interface ITransactionRepository
    {
        Task<Transaction> Save(Transaction transaction);
        Task<IEnumerable<Transaction>> All();
    }
}
