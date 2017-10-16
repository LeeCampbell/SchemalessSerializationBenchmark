using System;

namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    public interface ILoanEvent : IEvent
    {
        Guid CustomerId { get; }
        LoanType LoanType { get; }
        LoanSubType LoanSubType { get; }
        Guid OriginatingStoreId { get; }
        Guid FundingStoreId { get; }
        bool IsLoanFailed { get; }
        bool IsLoanWithCollectionAgent { get; }
        bool IsLoanWrittenOff { get; }
    }
}