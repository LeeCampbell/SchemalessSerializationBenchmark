using System.ComponentModel;

namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    public enum LoanSubType
    {
        [Description("Unknown")]
        Unknown = 0,
        [Description("Golden Client")]
        GoldenClient = 1,
        [Description("PL V2")]
        PlV2 = 2,
        [Description("Secured")]
        Secured = 3,
        [Description("Six Hundred")]
        SixHundred = 4,
        [Description("Unsecured")]
        Unsecured = 5,
        [Description("Brokered Cash Advance")]
        Cab = 6,
        [Description("Cash Advance")]
        CashAdvance = 7,
        [Description("Cash Advance")]
        CashAdvFmf = 8,
        [Description("Pawn")]
        Pawn = 9,
        [Description("Macc")]
        MaccV1 = 10,
        [Description("PL V3")]
        PlV3 = 11
    }
}