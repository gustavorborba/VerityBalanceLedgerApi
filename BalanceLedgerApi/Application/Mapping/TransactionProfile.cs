using AutoMapper;

namespace BalanceLedgerApi.Application.Mapping
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDto>();
            CreateMap<TransactionDto, Transaction>();
        }
    }
}
