using System;
using SchemalessSerializationBenchmark.Serialization.Contracts;

namespace SchemalessSerializationBenchmark.Serialization
{
    public sealed class MonthlyFeeChargedEventProxy
    {
        public Date BankingDate { get; set; }
        public Date EffectiveDate { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public decimal Amount { get; set; }
        public LedgerTransactionType TransactionType { get; set; }
    }
}
