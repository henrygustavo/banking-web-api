namespace Banking.Domain.Entity.Accounts
{
    using Banking.Domain.Entity.Customers;

    public class BankAccount
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Balance { get; set; }
        public bool IsLocked { get; set; }
        public Customer Customer { get; set; }

    }
}
