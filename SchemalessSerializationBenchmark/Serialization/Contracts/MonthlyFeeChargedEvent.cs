using System;

namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    //[MercuryEventIdentifier(AggregateType.Loan, LoanEvents.MonthlyFeeCharged)]
    public class MonthlyFeeChargedEvent : LoanEventBase, ILoanLedgerEvent, IRepaymentScheduleChangedEvent
    {
        public MonthlyFeeChargedEvent()
        {
            TransactionType = LedgerTransactionType.MonthlyFee;
        }

        public Date DueDate { get; set; }
        public decimal Balance { get; set; }
        public TransactionAllocation Allocation { get; set; }

        public decimal MonthlyFee { get; set; }
        public decimal Amount { get; set; }
        public decimal AmountNotCharged { get; set; }

        public LedgerTransactionType TransactionType { get; set; }
        public DateTimeOffset TransactionDate { get; set; }
        public Date BankingDate { get; set; }
        public Date EffectiveDate { get; set; }
        public Guid TransactionId { get; set; }
        public bool IsContractedFee { get; set; }
        public ScheduledRepayment[] RepaymentSchedule { get; set; }

        public ExemptionReason? Exemption { get; set; }

        public enum ExemptionReason
        {
            FeeLimit = 1,
            MonthlyFeesStopped = 2,
            OnlyDefaultFeesRemaining = 3,
            NoOutstandingBalance = 4,                                       // Loan will be settled once direct debit payments clear
            OutstandingBalanceLessThanSmallBalanceDiscountThreshold = 5     // Loan will be settled once direct debit payments clear
        }
    }
}