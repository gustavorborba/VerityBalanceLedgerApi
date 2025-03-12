using BalanceLedgerApi.Domain.Model;

namespace BalanceLedgerApi.Application.Interfaces.Repository
{
    public interface ITransactionRepository
    {
        Task<Transaction> Save(Transaction transaction);
        Task<IEnumerable<Transaction>> All();
    }
}
