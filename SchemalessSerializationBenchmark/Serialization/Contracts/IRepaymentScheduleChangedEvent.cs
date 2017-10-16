namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    public interface IRepaymentScheduleChangedEvent : ILoanEvent, IHasRepaymentSchedule
    {
    }
}