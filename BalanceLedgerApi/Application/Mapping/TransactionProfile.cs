using AutoMapper;
using BalanceLedgerApi.Application.Dto;
using BalanceLedgerApi.Domain.Model;

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
