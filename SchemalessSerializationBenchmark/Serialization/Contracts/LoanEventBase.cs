using System;

namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    public abstract class LoanEventBase : Event, ILoanEvent
    {
        public Guid CustomerId { get; set; }
        public LoanType LoanType { get; set; }
        public LoanSubType LoanSubType { get; set; }
        public Guid OriginatingStoreId { get; set; }
        public Guid FundingStoreId { get; set; }
        public bool IsLoanFailed { get; set; }
        public bool IsLoanWrittenOff { get; set; }
        public bool IsLoanWithCollectionAgent { get; set; }
    }
}