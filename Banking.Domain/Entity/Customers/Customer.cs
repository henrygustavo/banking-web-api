namespace Banking.Domain.Entity.Customers
{
    using Banking.Domain.Entity.Accounts;
    using System.Collections.Generic;

    public class Customer
    {
        public Customer()
        {
            BankAccounts = new HashSet<BankAccount>();
        }
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}

