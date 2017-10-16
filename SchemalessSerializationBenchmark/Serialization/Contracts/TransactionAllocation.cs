namespace SchemalessSerializationBenchmark.Serialization.Contracts
{
    public class TransactionAllocation
    {
        public decimal InterestAmount { get; set; }
        public decimal DefaultFeeAmount { get; set; }
        public decimal PrincipalAmount { get; set; }
        public decimal OverpaymentAmount { get; set; }

        public decimal TotalAmount()
        {
            return InterestAmount + DefaultFeeAmount + PrincipalAmount + OverpaymentAmount;
        }

        public static TransactionAllocation operator +(TransactionAllocation a, TransactionAllocation b)
        {
            return new TransactionAllocation
            {
                InterestAmount = a.InterestAmount + b.InterestAmount,
                DefaultFeeAmount = a.DefaultFeeAmount + b.DefaultFeeAmount,
                PrincipalAmount = a.PrincipalAmount + b.PrincipalAmount,
                OverpaymentAmount = a.OverpaymentAmount + b.OverpaymentAmount
            };
        }
    }
}