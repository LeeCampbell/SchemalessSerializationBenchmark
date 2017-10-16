using System;
using System.ComponentModel;

namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    // Reversals are used to represent undoing transactions made (generally) in error
    // Whereas Refunds are used when an actual Refund occurs 
    public enum LedgerTransactionType
    {
        [Description("Disbursement")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Disbursement)]
        Disbursement = 1,

        [Description("Disbursement Reversal")]
        [OriginalLedgerTransaction(Disbursement)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        DisbursementReversal = 2,

        [Description("Establishment Fee")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Fee)]
        EstablishmentFee = 3,

        [Description("Monthly Fee")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Fee)]
        MonthlyFee = 4,

        [Description("Monthly Fee Reversal")]
        [OriginalLedgerTransaction(MonthlyFee)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        MonthlyFeeReversal = 5,

        [Description("Direct Debit Repayment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Repayment)]
        DirectDebitRepayment = 6,

        [Description("Direct Debit Repayment Reversal")]
        [OriginalLedgerTransaction(DirectDebitRepayment)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        DirectDebitRepaymentReversal = 7,

        [Description("Default Fee")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Fee)]
        DefaultFee = 8,

        [Description("Default Fee Reversal")]
        [OriginalLedgerTransaction(DefaultFee)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        DefaultFeeReversal = 9,

        [Description("BPAY Repayment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Repayment)]
        BPayRepayment = 12,

        //   Domain does not require a BPay Reversal event
        [Description("BPAY Repayment Reversal")]
        [OriginalLedgerTransaction(BPayRepayment)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        [ImportExclusive]
        BPayRepaymentReversal = 13,

        [Description("Establishment Fee Reversal")]
        [OriginalLedgerTransaction(EstablishmentFee)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        [ImportExclusive]
        EstablishmentFeeReversal = 14,

        [Description("Import Incompatible Transaction")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Adjustment)]
        ImportIncompatibleTransaction = 18,

        [Description("Import Admin Adjustment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Adjustment)]
        ImportAdminAdjustment = 19,

        [Description("Import Transfer In")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Transfer)]
        ImportTransferIn = 20,

        [Description("Import Transfer Out")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Transfer)]
        ImportTransferOut = 21,

        [Description("Import Refund")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Refund)]
        ImportRefund = 22,

        [Description("Cash Repayment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Repayment)]
        CashRepayment = 23,

        [Description("Eftpos Repayment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Repayment)]
        EftposRepayment = 24,

        [Description("Debit Card Repayment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Repayment)]
        DebitCardRepayment = 25,

        [Description("Cheque Repayment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Repayment)]
        ChequeRepayment = 26,

        [Description("Direct Deposit Repayment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Repayment)]
        DirectDepositRepayment = 27,

        [Description("Discount")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Adjustment)]
        Discount = 28,

        [Description("Collection Agent Fee")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Fee)]
        CollectionFee = 29,

        [Description("Default Fee Waiver")]
        [OriginalLedgerTransaction(DefaultFee)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.FeeWaiver)]
        DefaultFeeWaiver = 30,

        [Description("Monthly Fee Waiver")]
        [OriginalLedgerTransaction(MonthlyFee)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.FeeWaiver)]
        MonthlyFeeWaiver = 31,

        [Description("Cash Refund")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Refund)]
        CashRefund = 32,

        [Description("EFTPOS Refund")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Refund)]
        EftposRefund = 33,

        [Description("Banking Refund")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Refund)]
        BankingRefund = 34,

        [Description("Collection Agency Repayment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Repayment)]
        CollectionAgencyRepayment = 35,

        [Description("Collection Agency Withheld Repayment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Repayment)]
        CollectionAgencyWithheldRepayment = 36,

        [Description("Collection Agency Fee Removed")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Adjustment)]
        CollectionAgencyFeeRemoval = 37,

        [Description("Small Balance Discount")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Adjustment)]
        SmallBalanceDiscount = 38,

        [Description("Small Balance Discount Reversed")]
        [OriginalLedgerTransaction(SmallBalanceDiscount)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        SmallBalanceDiscountReversal = 39,

        [Description("Write Off")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Adjustment)]
        WriteOff = 40,

        [Description("Card Fee")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Fee)]
        CardFee = 41,

        [Description("Import Breakdown Adjustment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Adjustment)]
        ImportAdjustment = 42,

        [Description("Debit Card Repayment Reversal")]
        [OriginalLedgerTransaction(DebitCardRepayment)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        DebitCardRepaymentReversal = 43,

        [Description("Direct Deposit Repayment Reversal")]
        [OriginalLedgerTransaction(DirectDepositRepayment)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        DirectDepositRepaymentReversal = 44,

        [Description("Cheque Repayment Reversal")]
        [OriginalLedgerTransaction(ChequeRepayment)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        ChequeRepaymentReversal = 45,

        [Description("Import Rounding Adjustment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Adjustment)]
        ImportRoundingAdjustment = 46,

        [Description("Collection Agency Withheld Repayment Refund")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Refund)]
        CollectionAgencyWithheldRepaymentRefund = 47,

        [Description("Import Interest Adjustment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Adjustment)]
        ImportInterestAdjustment = 48,

        [Description("Default Fee Waiver Reversal")]
        [OriginalLedgerTransaction(DefaultFeeWaiver)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        DefaultFeeWaiverReversal = 49,

        [Description("Monthly Fee Waiver Reversal")]
        [OriginalLedgerTransaction(MonthlyFeeWaiver)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        MonthlyFeeWaiverReversal = 50,

        [Description("Collection Agency Withheld Repayment Reversal")]
        [OriginalLedgerTransaction(CollectionAgencyWithheldRepayment)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        CollectionAgencyWithheldRepaymentReversal = 51,

        [Description("Discount Reversal")]
        [OriginalLedgerTransaction(Discount)]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        DiscountReversal = 52,

        [Description("Interest Charge")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Fee)]
        InterestFee = 53,

        [Description("Interest Reversal")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Fee)]
        InterestFeeReversal = 54,

        [Description("Principal Adjustment")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Adjustment)]
        PrincipalAdjustment = 55,

        [Description("Refund Reversal")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Reversal)]
        BankingRefundReversal = 56,

        [ReadModelOnly]
        [Description("Establishment Fee Amortisation")]
        [LedgerTransactionCategory(LoanLedgerTransactionCategory.Adjustment)]
        EstablishmentFeeAmortisation = 57
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class LedgerTransactionCategoryAttribute : Attribute
    {
        public LoanLedgerTransactionCategory Value { get; private set; }

        public LedgerTransactionCategoryAttribute(LoanLedgerTransactionCategory value)
        {
            Value = value;
        }
    }

    public enum LoanLedgerTransactionCategory
    {
        [Description("Fee")]
        Fee,

        [Description("Repayment")]
        Repayment,

        [Description("Reversal")]
        Reversal,

        [Description("Transfer")]
        Transfer,

        [Description("Disbursement")]
        Disbursement,

        [Description("Refund")]
        Refund,

        [Description("Adjustment")]
        Adjustment,

        [Description("FeeWaiver")]
        FeeWaiver,
    }


    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class OriginalLedgerTransactionAttribute : Attribute
    {
        public LedgerTransactionType Value { get; private set; }

        public OriginalLedgerTransactionAttribute(LedgerTransactionType value)
        {
            Value = value;
        }
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class ReadModelOnlyAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public class ImportExclusiveAttribute : Attribute
    { }
}