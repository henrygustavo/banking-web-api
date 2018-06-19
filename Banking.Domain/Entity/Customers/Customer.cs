namespace Banking.Domain.Entity.Customers
{
    using Banking.Domain.Entity.Accounts;
    using System.Collections.Generic;

    public class Customer
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<BankAccount> BankAccounts { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}

