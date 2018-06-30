namespace Banking.Domain.Entity.Customers
{
    using Accounts;
    using System.Collections.Generic;
    using Identities;

    public class Customer
    {
        public Customer()
        {
            BankAccounts = new HashSet<BankAccount>();
        }
        public int Id { get; set; }
        public string Dni { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public int IdentityUserId { get; set; }
        public IdentityUser IdentityUser { get; set; }
        public ICollection<BankAccount> BankAccounts { get; set; }
    }
}

