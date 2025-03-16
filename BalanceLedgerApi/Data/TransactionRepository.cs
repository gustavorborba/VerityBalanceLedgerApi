using MongoDB.Driver;

namespace BalanceLedgerApi.Data
{
    public class TransactionRepository(IMongoDatabase database, ILogger<TransactionRepository> _logger) : ITransactionRepository
    {
        private readonly IMongoCollection<Transaction> _context = database.GetCollection<Transaction>("Transactions");

        public async Task<IEnumerable<Transaction>> All() => await _context.Find(_ => true).ToListAsync();

        public async Task<Transaction> Save(Transaction transaction)
        {
            await _context.InsertOneAsync(transaction);

            _logger.LogInformation("Saved Transaction");
            return transaction;
        }
    }
}
