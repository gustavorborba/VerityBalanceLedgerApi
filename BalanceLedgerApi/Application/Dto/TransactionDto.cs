﻿namespace BalanceLedgerApi.Application.Dto
{
    public class TransactionDto
    {
        public TransactionType Type { get; set; }
        public decimal Value { get; set; }
    }
}
