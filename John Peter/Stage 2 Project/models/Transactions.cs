namespace Banking.models
{
    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }

    public record Transaction
    {
        public Guid Id { get; set; }

        public TransactionType? Type { get; set; }
        public double Amount { get; set; }
        public double Balance { get; set; }
        public DateTimeOffset Created { get; set; }
    };
}
