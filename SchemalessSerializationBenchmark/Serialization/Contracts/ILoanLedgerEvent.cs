using System;

namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    public interface ILoanLedgerEvent : ILoanEvent
    {
        Guid TransactionId { get; }

        LedgerTransactionType TransactionType { get; }

        /// <summary>
        // The time this transaction was initiated, in the timezone of the entity that initiated the transaction.
        // NOTE: This may be before it was recorded in the ledger.
        // e.g. A payment transaction may have been taken in store at 10PM in western australia on the 1/1/2000.
        /// </summary>
        DateTimeOffset TransactionDate { get; }

        /// <summary>
        /// The date that this transaction appeared on the relevant bank statement. This impacts reporting periods that are aligned with banking i.e. banking reconciliation.
        /// </summary>
        Date BankingDate { get; set; }

        /// <summary>
        /// The date that this transaction is effective in terms of the loan domain. This could impact the application of fees & interest.
        /// </summary>
        Date EffectiveDate { get; set; }

        /// <summary>
        /// For anyone else confused by directionality, the convention seems to be:
        ///   +ve amounts indicate a increase in loan balance
        ///   -ve amounts indicate a reduction in loan balance
        /// 
        ///  This is the case for all ILoanLedgerEvents even if the concrete type suggests an inverted relationship
        ///  e.g.: Disbursement events always have a negative amount
        ///  e.g.: Repayment events always have a positive amount
        /// 
        /// </summary>
        decimal Amount { get; set; }

        /// <summary>
        /// For anyone else confused by directionality, the convention seems to be:
        ///   +ve amounts indicate the loan is overpaid
        ///   -ve amounts indicate the loan is active (has outstanding balance to be paid)
        /// </summary>
        decimal Balance { get; set; }

        /// <summary>
        /// The breakdown of the balances delta for interest based loans.
        /// </summary>
        TransactionAllocation Allocation { get; set; }
    }
}