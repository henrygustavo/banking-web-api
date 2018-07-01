namespace Banking.Domain.Repository.Accounts
{
    using Banking.Domain.Entity.Accounts;
    using Common;
    using System.Collections.Generic;

    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        string GenerateAccountNumber();

        IEnumerable<BankAccount> GetAllWithCustomers(int pageNumber, int pageSize,
            string orderBy, string orderDirection);

        BankAccount GetWithCustomer(int id);
    }
}
