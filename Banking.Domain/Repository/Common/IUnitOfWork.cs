namespace Banking.Domain.Repository.Common
{
    using Accounts;
    using Customers;
    using System;

    public interface IUnitOfWork : IDisposable
    {
        ICustomerRepository Customers { get; }
        IBankAccountRepository BankAccounts { get; }
        int Complete();
    }
}
