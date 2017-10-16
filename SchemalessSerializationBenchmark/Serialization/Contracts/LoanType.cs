using System.ComponentModel;

namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    public enum LoanType
    {
        [Description("")]
        None = 0,

        [Description("Cash Advance")]
        CashAdvance = 1,

        [Description("Personal Loan")]
        PersonalLoan = 2,

        [Description("MACC")]
        Macc = 3
    }
}