namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    public interface IHasRepaymentSchedule : IEvent
    {
        ScheduledRepayment[] RepaymentSchedule { get; set; }
    }
}