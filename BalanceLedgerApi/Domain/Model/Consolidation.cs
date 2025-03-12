namespace BalanceLedgerApi.Domain.Model
{
    public class Consolidation : BaseEntity
    {
        public decimal Value { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
