using System;

namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    [Serializable]
    public class ScheduledRepayment
    {
        public Guid Id { get; set; }
        public Date RepaymentDate { get; set; }
        public decimal AmountDue { get; set; }
        public decimal AmountOutstanding { get; set; }
        public ScheduledRepaymentStatus Status { get; set; }

        public ScheduledRepayment Clone()
        {
            return new ScheduledRepayment
            {
                Id = Id,
                RepaymentDate = RepaymentDate,
                AmountDue = AmountDue,
                AmountOutstanding = AmountOutstanding,
                Status = Status
            };
        }
    }
}