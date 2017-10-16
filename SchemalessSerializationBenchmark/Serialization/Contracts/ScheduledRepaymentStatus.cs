namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    public enum ScheduledRepaymentStatus
    {
        Scheduled = 1,
        Repaid = 2,
        Missed = 3,

        // Direct Debit Specific
        DdrPresented = 4,
        DdrRepaid = 5,
        DdrDeclined = 6
    }
}